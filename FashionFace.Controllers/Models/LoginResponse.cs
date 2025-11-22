using System;

namespace FashionFace.Controllers.Models;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);