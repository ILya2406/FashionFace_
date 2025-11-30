using System;

namespace FashionFace.Controllers.Anonymous.Responses.Models.Authentication;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);