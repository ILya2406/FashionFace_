using System;

namespace FashionFace.Controllers.Anonymous.Responses.Models.Authentication;

public sealed record RefreshResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);