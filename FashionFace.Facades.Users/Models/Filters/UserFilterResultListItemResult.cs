using System;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterResultListItemResult(
    Guid TalentId,
    string AvatarRelativePath
);