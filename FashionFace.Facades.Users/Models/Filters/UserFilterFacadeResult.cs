using System;
using System.Collections.Generic;

using FashionFace.Facades.Users.Models.AppearanceTraits;
using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterFacadeResult(
    Guid Id,
    string Name,
    double PositionIndex,
    TalentType? TalentType,
    UserFilterLocationListItemResult? Location,
    UserAppearanceTraitsResult? AppearanceTraits,
    IReadOnlyList<Guid> TagList
);