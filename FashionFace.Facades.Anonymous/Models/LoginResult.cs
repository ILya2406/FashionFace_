using System;

namespace FashionFace.Facades.Anonymous.Models;

public sealed record LoginResult(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);