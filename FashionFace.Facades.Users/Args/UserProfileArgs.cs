using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserProfileArgs(
    Guid UserId,
    Guid ProfileId
);