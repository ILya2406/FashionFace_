using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Facades.Args;
using FashionFace.Facades.Domains.Args;
using FashionFace.Facades.Domains.Interfaces;
using FashionFace.Facades.Interfaces;
using FashionFace.Facades.Models;

using Microsoft.IdentityModel.Tokens;

using static FashionFace.Common.Exceptions.Constants.ExceptionConstants;

namespace FashionFace.Facades.Implementations;

public sealed class RefreshFacade(
    IExceptionDescriptor exceptionDescriptor,
    IJwtSettingsFactory jwtSettingsFactory,
    IAuthenticationModelCreateFacade authenticationModelCreateFacade
) : IRefreshFacade
{
    public async Task<RefreshResult> Execute(RefreshArgs args)
    {
        var refreshToken =
            args.RefreshToken;

        if (refreshToken.IsEmpty())
        {
            throw exceptionDescriptor.Exception(
                InvalidRefreshToken
            );
        }

        var jwtSettings =
            jwtSettingsFactory.GetSettings();

        var tokenHandler =
            new JwtSecurityTokenHandler();

        var symmetricSecurityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    jwtSettings.Secret
                )
            );

        var validationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = symmetricSecurityKey,
                ClockSkew = TimeSpan.Zero,
            };

        var principal =
            tokenHandler
                .ValidateToken(
                    refreshToken,
                    validationParameters,
                    out var validatedToken
                );

        if (principal is null)
        {
            throw exceptionDescriptor.Exception(
                InvalidRefreshToken
            );
        }

        var isInvalidToken =
            validatedToken is not JwtSecurityToken jwt
            || jwt.Header?.Alg != SecurityAlgorithms.HmacSha256;

        if (isInvalidToken)
        {
            throw exceptionDescriptor.Exception(
                InvalidRefreshToken
            );
        }

        var idClaim =
            principal
                .FindFirst(
                    ClaimTypes.NameIdentifier
                );

        if (idClaim is null)
        {
            throw exceptionDescriptor.Exception(
                InvalidRefreshToken
            );
        }

        var emailClaim =
            principal
                .FindFirst(
                    ClaimTypes.Email
                );

        if (emailClaim is null)
        {
            throw exceptionDescriptor.Exception(
                InvalidRefreshToken
            );
        }

        var userId =
            Guid.Parse(
                idClaim.Value
            );

        var authenticationModelCreateArgs =
            new AuthenticationModelCreateArgs(
                userId,
                emailClaim.Value
            );

        var authenticationModel =
            await
                authenticationModelCreateFacade
                    .Execute(
                        authenticationModelCreateArgs
                    );

        var result =
            new RefreshResult(
                authenticationModel.AccessToken,
                authenticationModel.RefreshToken,
                authenticationModel.AccessTokenExpireAt,
                authenticationModel.RefreshTokenExpireAt
            );

        return
            result;
    }
}