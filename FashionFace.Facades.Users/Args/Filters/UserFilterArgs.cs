using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterArgs(
    Guid UserId,
    Guid FilterId
);