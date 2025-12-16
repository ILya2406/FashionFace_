using System;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterListItemResponse(
    Guid Id,
    double PositionIndex,
    string Name
);