using System;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterCreateArgs(
    Guid UserId,
    string Name
);