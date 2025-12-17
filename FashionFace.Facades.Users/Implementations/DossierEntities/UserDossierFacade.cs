using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Facades.Users.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor
) : IUserDossierFacade
{
    public async Task<UserDossierResult> Execute(
        UserDossierArgs args
    )
    {
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
                            == args.UserId
                    );

        if (dossier is null)
        {
            throw exceptionDescriptor.NotFound<Dossier>();
        }

        var result =
            new UserDossierResult(
                dossier.Id
            );

        return
            result;
    }
}