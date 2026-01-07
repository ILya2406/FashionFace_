using System;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterResultListItemResponse(
    Guid ProfileId,
    Guid TalentId,
    string Name,
    string AvatarRelativePath,
    string? SexType,
    int? Height,
    string? HairColorType,
    string? EyeColorType
);