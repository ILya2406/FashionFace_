using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserPortfolioArgs(
    Guid UserId,
    Guid TalentId
);