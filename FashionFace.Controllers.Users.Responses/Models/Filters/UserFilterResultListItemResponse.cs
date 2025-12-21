using System;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterResultListItemResponse(
    Guid TalentId,
    string AvatarRelativePath
);