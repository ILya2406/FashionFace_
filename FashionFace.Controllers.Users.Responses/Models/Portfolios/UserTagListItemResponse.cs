using System;

namespace FashionFace.Controllers.Users.Responses.Models.Portfolios;

public sealed record UserTagListItemResponse(
    Guid Id,
    double PositionIndex
);