using System;

namespace FashionFace.Controllers.Anonymous.Responses.Models;

public sealed record RefreshResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);