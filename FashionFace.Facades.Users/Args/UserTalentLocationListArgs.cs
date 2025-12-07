using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserTalentLocationListArgs(
    Guid UserId,
    Guid TalentId
);