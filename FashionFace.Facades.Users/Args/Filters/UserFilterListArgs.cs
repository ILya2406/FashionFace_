using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterListArgs(
    Guid UserId
);