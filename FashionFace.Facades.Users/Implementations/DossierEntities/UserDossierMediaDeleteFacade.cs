using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierMediaDeleteFacade(
    IGenericReadRepository genericReadRepository,
    IDeleteRepository deleteRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserDossierMediaDeleteFacade
{
    public async Task Execute(
        UserDossierMediaDeleteArgs args
    )
    {
        var (
            userId,
            mediaId
            ) = args;

        var dossierMediaAggregateCollection =
            genericReadRepository.GetCollection<DossierMediaAggregate>();

        var dossierMediaAggregate =
            await
                dossierMediaAggregateCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.MediaAggregateId == mediaId
                            && entity
                                .Dossier!
                                .Profile!
                                .ApplicationUserId
                            == userId
                    );

        if (dossierMediaAggregate is null)
        {
            throw exceptionDescriptor.NotFound<DossierMediaAggregate>();
        }

        await
            deleteRepository
                .DeleteAsync(
                    dossierMediaAggregate
                );
    }
}