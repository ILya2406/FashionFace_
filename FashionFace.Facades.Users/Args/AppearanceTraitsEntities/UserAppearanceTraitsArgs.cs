using System;

namespace FashionFace.Facades.Users.Args.AppearanceTraitsEntities;

public sealed record UserAppearanceTraitsArgs(
    Guid UserId,
    Guid ProfileId
);