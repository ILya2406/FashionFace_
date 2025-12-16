using System;

namespace FashionFace.Facades.Users.Models.Portfolios;

public sealed record UserTagListItemResult(
    Guid Id,
    double PositionIndex
);