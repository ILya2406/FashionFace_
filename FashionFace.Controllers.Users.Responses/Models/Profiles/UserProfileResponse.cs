using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.Profiles;

public sealed record UserProfileResponse(
    Guid Id,
    string Name,
    string? City,
    string Description,
    AgeCategoryType AgeCategoryType,
    DateTime CreatedAt
);