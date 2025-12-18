using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterResultStatusArgs(
    Guid UserId,
    Guid FilterId
);