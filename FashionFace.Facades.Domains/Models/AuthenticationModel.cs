namespace FashionFace.Facades.Domains.Models;

public sealed record AuthenticationModel(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpireAt,
    DateTime RefreshTokenExpireAt
);