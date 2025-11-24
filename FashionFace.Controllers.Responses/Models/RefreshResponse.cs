using System;

namespace FashionFace.Controllers.Responses.Models;

public sealed record RefreshResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiresAt
);