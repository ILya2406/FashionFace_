using System;

namespace FashionFace.Facades.Users.Args.Locations;

public sealed record UserLocationListArgs(
    Guid UserId,
    Guid TalentId
);