using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Facades.Admins.Args;
using FashionFace.Facades.Admins.Interfaces;
using FashionFace.Facades.Admins.Models;
using FashionFace.Repositories.Context.Models;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;

using static FashionFace.Common.Constants.Constants.UserRoleConstants;
using static FashionFace.Common.Exceptions.Constants.ExceptionConstants;

namespace FashionFace.Facades.Admins.Implementations;

public sealed class UserCreateFacade(
    IUserManagerDecorator userManagerDecorator,
    IRoleManagerDecorator roleManagerDecorator,
    IExceptionDescriptor exceptionDescriptor,
    ICreateRepository createRepository,
    ITransactionManager transactionManager
) : IUserCreateFacade
{
    public async Task<UserCreateResult> Execute(UserCreateArgs args)
    {
        var (
            _,
            email,
            userName,
            password,
            name,
            description,
            ageCategoryType
            ) = args;

        var existingByEmail =
            await
                userManagerDecorator
                    .FindByEmailAsync(
                        email
                    );

        if (existingByEmail is not null)
        {
            throw exceptionDescriptor.Exception(
                EmailAlreadyExists
            );
        }

        var existingByName =
            await
                userManagerDecorator
                    .FindByNameAsync(
                        userName
                    );

        if (existingByName is not null)
        {
            throw exceptionDescriptor.Exception(
                UsernameAlreadyExists
            );
        }

        using var transaction =
            await
                transactionManager.BeginTransaction();

        var applicationUser =
            new ApplicationUser
            {
                Email = email,
                UserName = userName,
            };

        var createResult =
            await
                userManagerDecorator
                    .CreateAsync(
                        applicationUser,
                        password
                    );

        if (!createResult.Succeeded)
        {
            throw exceptionDescriptor.IdentityErrorList(
                createResult.Errors
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

        var profile =
            new Profile
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = applicationUser.Id,
                CreatedAt = DateTime.UtcNow,
                AgeCategoryType = ageCategoryType,
                Name = name,
                Description = description,
            };

        await
            createRepository
                .CreateAsync(
                    profile
                );

        await
            transaction.CommitAsync();

        var result =
            new UserCreateResult(
                applicationUser.Id
            );

        return
            result;
    }
}