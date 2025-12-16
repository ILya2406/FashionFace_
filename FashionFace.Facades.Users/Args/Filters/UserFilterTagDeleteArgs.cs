using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterTagDeleteArgs(
    Guid UserId,
    Guid FilterId,
    Guid TagId
);