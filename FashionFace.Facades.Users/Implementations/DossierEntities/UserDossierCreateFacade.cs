using System.Threading.Tasks;

using System;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.DossierEntities;
using FashionFace.Facades.Users.Interfaces.DossierEntities;
using FashionFace.Repositories.Context.Models.DossierEntities;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.DossierEntities;

public sealed class UserDossierCreateFacade(
    IGenericReadRepository genericReadRepository,
    ICreateRepository createRepository,
    IExceptionDescriptor exceptionDescriptor
): IUserDossierCreateFacade
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
                Id = Guid.NewGuid(),
                ProfileId = profile.Id,
                IsDeleted = false,
            };

        await
            createRepository
                .CreateAsync(
                    newDossier
                );
    }
}