using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierCreateFacade(
    IGenericReadRepository genericReadRepository,
    IUpdateRepository updateRepository,
    IExceptionDescriptor exceptionDescriptor,
    IGuidGenerator guidGenerator
) : IUserDossierCreateFacade
{
    public async Task Execute(
        UserDossierCreateArgs args
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

        if (dossier is not null)
        {
            throw exceptionDescriptor.Exists<Dossier>();
        }

        var profileCollection =
            genericReadRepository.GetCollection<Profile>();

        var profile =
            await
                profileCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ApplicationUserId == args.UserId
                    );

        if (profile is null)
        {
            throw exceptionDescriptor.NotFound<Profile>();
        }

        var newDossier =
            new Dossier
            {
                Id = guidGenerator.GetNew(),
                ProfileId = profile.Id,
                IsDeleted = false,
            };

        await
            updateRepository
                .UpdateAsync(
                    newDossier
                );
    }
}