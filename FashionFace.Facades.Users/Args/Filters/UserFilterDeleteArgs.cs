using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterDeleteArgs(
    Guid UserId,
    Guid FilterId
);