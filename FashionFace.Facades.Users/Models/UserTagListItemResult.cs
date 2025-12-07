using System;

namespace FashionFace.Facades.Users.Models;

public sealed record UserTagListItemResult(
    Guid Id,
    int PositionIndex,
    string Name
);