using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record UserFilterUpdateArgs(
    Guid UserId,
    Guid FilterId,
    string? Name,
    double? PositionIndex,
    TalentType? TalentType,
    FilterLocationArgs? FilterLocation,
    FilterAppearanceTraitsArgs? FilterAppearanceTraits
);