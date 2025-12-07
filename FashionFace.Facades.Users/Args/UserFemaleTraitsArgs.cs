using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserFemaleTraitsArgs(
    Guid UserId,
    Guid ProfileId
);