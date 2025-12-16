using System;

namespace FashionFace.Facades.Users.Args.Locations;

public sealed record UserLocationDeleteArgs(
    Guid UserId,
    Guid LocationId
);