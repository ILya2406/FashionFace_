using System;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserTagListItemResponse(
    Guid Id,
    double PositionIndex,
    string Name
);