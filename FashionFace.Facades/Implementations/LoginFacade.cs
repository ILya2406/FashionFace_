using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Facades.Args;
using FashionFace.Facades.Domains.Args;
using FashionFace.Facades.Domains.Interfaces;
using FashionFace.Facades.Interfaces;
using FashionFace.Facades.Models;

using static FashionFace.Common.Exceptions.Constants.ExceptionConstants;

namespace FashionFace.Facades.Implementations;

public sealed class LoginFacade(
    IUserManagerDecorator userManagerDecorator,
    IExceptionDescriptor exceptionDescriptor,
    IAuthenticationModelCreateFacade authenticationModelCreateFacade
) : ILoginFacade
{
    public async Task<LoginResult> Execute(
        LoginArgs args
    )
    {
        var (
            username,
            password
            ) = args;

        var user =
            await
                userManagerDecorator
                    .FindByEmailAsync(
                        username
                    );

        if (user is null)
        {
            user =
                await
                    userManagerDecorator
                        .FindByNameAsync(
                            username
                        );

            if (user is null)
            {
                throw exceptionDescriptor.Exception(
                    InvalidCredentials
                );
            }
        }

        var isValidPassword =
            await
                userManagerDecorator
                    .CheckPasswordAsync(
                        user,
                        password
                    );

        if (!isValidPassword)
        {
            throw exceptionDescriptor.Exception(
                InvalidCredentials
            );
        }

        var authenticationModelCreateArgs =
            new AuthenticationModelCreateArgs(
                user.Id,
                username
            );

        var authenticationModel =
            await
                authenticationModelCreateFacade
                    .Execute(
                        authenticationModelCreateArgs
                    );

        var result =
            new LoginResult(
                authenticationModel.AccessToken,
                authenticationModel.RefreshToken,
                authenticationModel.AccessTokenExpireAt,
                authenticationModel.RefreshTokenExpireAt
            );

        return
            result;
    }
}