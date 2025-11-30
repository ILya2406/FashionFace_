using System;

namespace FashionFace.Controllers.Anonymous.Responses.Models;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);