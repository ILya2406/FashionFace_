using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserMaleTraitsArgs(
    Guid UserId,
    Guid ProfileId
);