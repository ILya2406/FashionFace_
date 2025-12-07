using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models;

public sealed record UserTalentListItemResult(
    Guid Id,
    string Description,
    TalentType Type
);