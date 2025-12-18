using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterStartArgs(
    Guid UserId,
    Guid FilterId
);