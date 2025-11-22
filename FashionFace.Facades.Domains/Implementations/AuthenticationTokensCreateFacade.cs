using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using FashionFace.Services.ConfigurationSettings.Interfaces;
using FashionFace.Facades.Domains.Args;
using FashionFace.Facades.Domains.Interfaces;
using FashionFace.Facades.Domains.Models;

using Microsoft.IdentityModel.Tokens;

namespace FashionFace.Facades.Domains.Implementations;

public sealed class AuthenticationModelCreateFacade(
    IJwtSettingsFactory jwtSettingsFactory
) : IAuthenticationModelCreateFacade
{
    public async Task<AuthenticationModel> Execute(
        AuthenticationModelCreateArgs args
    )
    {
        var jwtSettings =
            jwtSettingsFactory
                .GetSettings();

        var bytes =
            Encoding
                .UTF8
                .GetBytes(
                    jwtSettings.Secret
                );

        var signinKey =
            new SymmetricSecurityKey(
                bytes
            );

        var claims =
            new List<Claim>
            {
                new(
                    ClaimTypes.NameIdentifier,
                    args.UserId.ToString()
                ),
                new(
                    ClaimTypes.Email,
                    args.Email
                ),
            };

        var refreshTokenExpiresAt =
            DateTime
                .UtcNow
                .AddDays(
                    jwtSettings.RefreshTokenExpiryDays
                );

        var signingCredentials =
            new SigningCredentials(
                signinKey,
                SecurityAlgorithms.HmacSha256
            );

        var refreshToken =
            new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                DateTime.UtcNow,
                refreshTokenExpiresAt,
                signingCredentials
            );

        var accessTokenExpiresAt =
            DateTime
                .UtcNow
                .AddMinutes(
                    jwtSettings.AccessTokenExpiryMinutes
                );

        var accessToken =
            new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                DateTime.UtcNow,
                accessTokenExpiresAt,
                signingCredentials
            );

        var handlerToken =
            new JwtSecurityTokenHandler();

        var accessTokenString =
            handlerToken
                .WriteToken(
                    accessToken
                );

        var refreshTokenString =
            handlerToken
                .WriteToken(
                    refreshToken
                );

        var resultAuth =
            new AuthenticationModel(
                accessTokenString!,
                refreshTokenString!,
                accessToken.ValidTo,
                refreshToken.ValidTo
            );

        return
            resultAuth;
    }
}