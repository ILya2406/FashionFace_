using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterTagCreateArgs(
    Guid UserId,
    Guid FilterId,
    Guid TagId
);