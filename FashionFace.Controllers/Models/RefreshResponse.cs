using System;

namespace FashionFace.Controllers.Models;

public sealed record RefreshResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);