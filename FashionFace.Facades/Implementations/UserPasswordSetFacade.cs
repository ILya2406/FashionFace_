using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Facades.Args;
using FashionFace.Facades.Interfaces;
using FashionFace.Repositories.Context.Models;

using static FashionFace.Common.Exceptions.Constants.ExceptionConstants;

namespace FashionFace.Facades.Implementations;

public sealed class UserPasswordSetFacade(
    IUserManagerDecorator userManagerDecorator,
    IExceptionDescriptor exceptionDescriptor
) : IUserPasswordSetFacade
{
    public async Task Execute(UserPasswordSetArgs args)
    {
        var (
            userId,
            oldPassword,
            newPassword
            ) = args;

        var user =
            await
                userManagerDecorator
                    .FindByIdAsync(
                        userId
                    );

        if (user is null)
        {
            throw exceptionDescriptor.NotFound<ApplicationUser>();
        }

        var validOldPassword =
            await
                userManagerDecorator
                    .CheckPasswordAsync(
                        user,
                        oldPassword
                    );

        if (!validOldPassword)
        {
            throw exceptionDescriptor.Exception(
                InvalidOldPassword
            );
        }

        var identityResult =
            await
                userManagerDecorator
                    .ChangePasswordAsync(
                        user,
                        oldPassword,
                        newPassword
                    );

        if (!identityResult.Succeeded)
        {
            var identityErrorCodeList =
                identityResult
                    .Errors
                    .Select(
                        error => $"{error.Code}"
                    )
                    .ToList();

            var data =
                new Dictionary<string, object>
                {
                    { "error", identityErrorCodeList },
                };

            throw exceptionDescriptor.Exception(
                IdentityErrors,
                data
            );
        }
    }
}