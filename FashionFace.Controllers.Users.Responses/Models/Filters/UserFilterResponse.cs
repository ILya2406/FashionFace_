using System;
using System.Collections.Generic;

using FashionFace.Controllers.Users.Responses.Models.AppearanceTraitsEntities;
using FashionFace.Controllers.Users.Responses.Models.Portfolios;
using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterResponse(
    Guid Id,
    string Name,
    double PositionIndex,
    TalentType? TalentType,
    UserFilterLocationListItemResponse? Location,
    UserAppearanceTraitsResponse? AppearanceTraits,
    IReadOnlyList<UserTagListItemResponse> TagList
);