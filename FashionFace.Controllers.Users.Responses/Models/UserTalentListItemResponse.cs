using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserTalentListItemResponse(
    Guid Id,
    string Description,
    TalentType Type
);