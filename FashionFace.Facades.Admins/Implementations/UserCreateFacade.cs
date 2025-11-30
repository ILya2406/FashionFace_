using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Facades.Admins.Args;
using FashionFace.Facades.Admins.Interfaces;
using FashionFace.Facades.Admins.Models;
using FashionFace.Repositories.Context.Models.IdentityEntities;

using static FashionFace.Common.Exceptions.Constants.ExceptionConstants;

namespace FashionFace.Facades.Admins.Implementations;

public sealed class UserCreateFacade(
    IUserManagerDecorator userManagerDecorator,
    IExceptionDescriptor exceptionDescriptor
) : IUserCreateFacade
{
    public async Task<UserCreateResult> Execute(UserCreateArgs args)
    {
        var (
            _,
            email,
            userName,
            password
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

        var user =
            new ApplicationUser
            {
                Email = email,
                UserName = userName,
            };

        var createResult =
            await
                userManagerDecorator
                    .CreateAsync(
                        user,
                        password
                    );

        if (!createResult.Succeeded)
        {
            throw exceptionDescriptor.IdentityErrorList(
                createResult.Errors
            );
        }

        var result =
            new UserCreateResult(
                user.Id
            );

        return
            result;
    }
}