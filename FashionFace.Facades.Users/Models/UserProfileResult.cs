using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models;

public sealed record UserProfileResult(
    string Name,
    string Description,
    AgeCategoryType AgeCategoryType,
    DateTime CreatedAt
);