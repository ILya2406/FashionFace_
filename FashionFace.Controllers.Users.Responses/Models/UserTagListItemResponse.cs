using System;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserTagListItemResponse(
    Guid Id,
    int PositionIndex,
    string Name
);