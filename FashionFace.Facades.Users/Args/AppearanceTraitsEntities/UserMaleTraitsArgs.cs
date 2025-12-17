using System;

namespace FashionFace.Facades.Users.Args.AppearanceTraitsEntities;

public sealed record UserMaleTraitsArgs(
    Guid UserId,
    Guid ProfileId
);