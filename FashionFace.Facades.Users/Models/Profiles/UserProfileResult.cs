using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.Profiles;

public sealed record UserProfileResult(
    Guid Id,
    string Name,
    string? City,
    string Description,
    AgeCategoryType AgeCategoryType,
    DateTime CreatedAt
);