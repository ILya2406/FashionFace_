using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Facades.Users.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
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

        // If dossier not found, create one
        if (dossier is null)
        {
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

            dossier = new Dossier
            {
                Id = Guid.NewGuid(),
                ProfileId = profile.Id,
                IsDeleted = false
            };

            await createRepository.CreateAsync(dossier);
        }

        var result =
            new UserDossierResult(
                dossier.Id
            );

        return
            result;
    }
}