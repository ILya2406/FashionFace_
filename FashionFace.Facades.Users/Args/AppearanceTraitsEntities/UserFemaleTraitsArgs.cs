using System;

namespace FashionFace.Facades.Users.Args.AppearanceTraitsEntities;

public sealed record UserFemaleTraitsArgs(
    Guid UserId,
    Guid ProfileId
);