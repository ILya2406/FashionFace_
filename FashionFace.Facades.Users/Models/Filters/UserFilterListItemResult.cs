using System;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterListItemResult(
    Guid Id,
    double PositionIndex,
    string Name
);