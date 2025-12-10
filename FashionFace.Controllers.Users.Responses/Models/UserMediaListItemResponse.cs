using System;
using System.Collections.Generic;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserMediaListItemResponse(
    Guid Id,
    double PositionIndex,
    string Description,
    string Url,
    IReadOnlyList<Guid> TagIdList
);