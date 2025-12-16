using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record UserFilterUpdateRequest(
    Guid FilterId,
    string? Name,
    double? PositionIndex,
    TalentType? TalentType,
    FilterLocationRequest? FilterLocation,
    FilterAppearanceTraitsRequest? FilterAppearanceTraits
);