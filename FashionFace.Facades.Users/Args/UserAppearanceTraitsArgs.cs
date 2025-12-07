using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserAppearanceTraitsArgs(
    Guid UserId,
    Guid ProfileId
);