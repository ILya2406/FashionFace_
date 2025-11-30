using System;

namespace FashionFace.Facades.Anonymous.Models;

public sealed record RefreshResult(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);