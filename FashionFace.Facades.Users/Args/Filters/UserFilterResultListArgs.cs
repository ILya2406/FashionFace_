using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterResultListArgs(
    Guid UserId,
    Guid FilterId,
    int Offset,
    int Count
);