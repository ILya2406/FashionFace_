using System;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterResultListItemResult(
    Guid ProfileId,
    Guid TalentId,
    string Name,
    string AvatarRelativePath,
    string? SexType,
    int? Height,
    string? HairColorType,
    string? EyeColorType
);