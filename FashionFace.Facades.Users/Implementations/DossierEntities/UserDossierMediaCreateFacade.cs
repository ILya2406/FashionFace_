using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierMediaCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor,
    IGuidGenerator guidGenerator
) : IUserDossierMediaCreateFacade
{
    public async Task Execute(
        UserDossierMediaCreateArgs args
    )
    {
        var (
            userId,
            mediaId
            ) = args;

        var dossierCollection =
            genericReadRepository.GetCollection<Dossier>();

        var dossier =
            await
                dossierCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (dossier is null)
        {
            throw exceptionDescriptor.NotFound<Dossier>();
        }

        var dossierId =
            dossier.Id;

        var dossierMediaAggregateCollection =
            genericReadRepository.GetCollection<DossierMediaAggregate>();

        var lastDossierMedia =
            await
                dossierMediaAggregateCollection
                    .Where(
                        entity =>
                            entity.DossierId == dossierId
                            && entity
                                .Dossier!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    )
                    .OrderByDescending(
                        entity => entity.PositionIndex
                    )
                    .FirstOrDefaultAsync();

        var lastPositionIndex =
            lastDossierMedia?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var newDossierMediaAggregate =
            new DossierMediaAggregate
            {
                Id = guidGenerator.GetNew(),
                PositionIndex = positionIndex,
                DossierId = dossierId,
                MediaAggregateId = mediaId,
            };

        await
            createRepository
                .CreateAsync(
                    newDossierMediaAggregate
                );
    }
}