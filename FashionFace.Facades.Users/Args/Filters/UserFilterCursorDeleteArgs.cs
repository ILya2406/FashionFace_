using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterCursorDeleteArgs(
    Guid UserId,
    Guid FilterId
);