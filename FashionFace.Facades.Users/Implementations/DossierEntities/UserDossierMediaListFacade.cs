using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Base.Models;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Facades.Users.Models.Portfolios;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierMediaListFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserDossierMediaListFacade
{
    public async Task<ListResult<UserMediaListItemResult>> Execute(
        UserDossierMediaListArgs args
    )
    {
        var dossierCollection =
            genericReadRepository.GetCollection<Dossier>();

        var dossier =
            await
                dossierCollection
                    .Include(
                        entity => entity.Profile
                    )
                    .Include(
                        entity => entity.DossierMediaCollection
                    )
                    .ThenInclude(
                        entity => entity.MediaAggregate
                    )
                    .ThenInclude(
                        entity => entity!.PreviewMedia
                    )
                    .ThenInclude(
                        entity => entity!.OptimizedFile
                    )
                    .ThenInclude(
                        entity => entity!.FileResource
                    )
                    .FirstOrDefaultAsync(
                        entity =>
                            entity
                                .Profile!
                                .ApplicationUserId
                            == args.UserId
                    );

        if (dossier is null)
        {
            throw exceptionDescriptor.NotFound<Dossier>();
        }

        var dossierMediaCollection =
            dossier.DossierMediaCollection;

        var mediaListResults =
            new List<UserMediaListItemResult>();

        foreach (var dossierMedia in dossierMediaCollection)
        {
            var optimizedFileUri =
                dossierMedia
                    .MediaAggregate!
                    .PreviewMedia!
                    .OptimizedFile!
                    .FileResource!
                    .RelativePath;

            var description =
                dossierMedia
                    .MediaAggregate
                    .Description;

            var userMediaListItemResult =
                new UserMediaListItemResult(
                    dossierMedia.MediaAggregateId,
                    dossierMedia.PositionIndex,
                    description,
                    optimizedFileUri
                );

            mediaListResults
                .Add(
                    userMediaListItemResult
                );
        }

        var result =
            new ListResult<UserMediaListItemResult>(
                dossierMediaCollection.Count,
                mediaListResults
            );

        return
            result;
    }
}