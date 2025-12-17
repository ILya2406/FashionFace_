using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.AppearanceTraitsEntities;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Context.Models.Profiles;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;

using static FashionFace.Common.Constants.Constants.UserRoleConstants;

namespace FashionFace.Facades.Anonymous.Implementations;

public sealed class RegisterFacade(
    IUserManagerDecorator userManagerDecorator,
    IRoleManagerDecorator roleManagerDecorator,
    IExceptionDescriptor exceptionDescriptor,
    ICreateRepository createRepository,
    ITransactionManager transactionManager
) : IRegisterFacade
{
    public async Task Execute(
        RegisterArgs args
    )
    {
        var (
            email,
            password
            ) = args;

        var identityFindByEmailResult =
            await
                userManagerDecorator
                    .FindByEmailAsync(
                        email
                    );

        if (identityFindByEmailResult is not null)
        {
            throw exceptionDescriptor.Exists<ApplicationUser>();
        }

        using var transaction =
            await
                transactionManager.BeginTransaction();

        var applicationUser =
            new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

        var identityCreateResult =
            await
                userManagerDecorator
                    .CreateAsync(
                        applicationUser,
                        password
                    );

        if (!identityCreateResult.Succeeded)
        {
            throw exceptionDescriptor.IdentityErrorList(
                identityCreateResult.Errors
            );
        }

        var addRoleResult =
            await
                roleManagerDecorator
                    .AddToRoleAsync(
                        applicationUser,
                        User
                    );

        if (!addRoleResult.Succeeded)
        {
            throw exceptionDescriptor.IdentityErrorList(
                addRoleResult.Errors
            );
        }

        var profileId =
            Guid.NewGuid();

        var appearanceTraitsId =
            Guid.NewGuid();

        var maleTraits =
            new MaleTraits
            {
                Id = Guid.NewGuid(),
                AppearanceTraitsId = appearanceTraitsId,
                FacialHairLengthType = HairLengthType.Undefined,
            };

        var femaleTraits =
            new FemaleTraits
            {
                Id = Guid.NewGuid(),
                AppearanceTraitsId = appearanceTraitsId,
                BustSizeType = BustSizeType.Undefined,
            };

        var appearanceTraits =
            new AppearanceTraits
            {
                Id = appearanceTraitsId,
                ProfileId = profileId,

                MaleTraits = maleTraits,
                FemaleTraits = femaleTraits,
            };

        // todo : validate empty Name & Description
        var profile =
            new Profile
            {
                Id = profileId,
                IsDeleted = false,
                ApplicationUserId = applicationUser.Id,
                CreatedAt = DateTime.UtcNow,
                AgeCategoryType = AgeCategoryType.Minor,
                Name = string.Empty,
                Description = string.Empty,

                AppearanceTraits =
                    appearanceTraits,
            };

        await
            createRepository
                .CreateAsync(
                    profile
                );

        await
            transaction.CommitAsync();
    }
}