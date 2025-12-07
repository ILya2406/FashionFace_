using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserTalentListArgs(
    Guid UserId,
    Guid ProfileId
);