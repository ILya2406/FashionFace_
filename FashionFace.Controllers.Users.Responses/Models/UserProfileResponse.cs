using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserProfileResponse(
    string Name,
    string Description,
    AgeCategoryType AgeCategoryType,
    DateTime CreatedAt
);