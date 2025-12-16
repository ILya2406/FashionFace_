using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.Identity.Interfaces;
using FashionFace.Facades.Anonymous.Args;
using FashionFace.Facades.Anonymous.Interfaces;
using FashionFace.Facades.Anonymous.Models;
using FashionFace.Facades.Domains.Args;
using FashionFace.Facades.Domains.Interfaces;

using static FashionFace.Common.Exceptions.Constants.ExceptionConstants;

namespace FashionFace.Facades.Anonymous.Implementations;

public sealed class LoginFacade(
    IUserManagerDecorator userManagerDecorator,
    IPasswordManagerDecorator passwordManagerDecorator,
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
                passwordManagerDecorator
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
                user.Email
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