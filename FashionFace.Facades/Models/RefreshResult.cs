using System;

namespace FashionFace.Facades.Models;

public sealed record RefreshResult(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt,
    DateTime RefreshTokenExpiresAt
);