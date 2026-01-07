using System;

namespace FashionFace.Facades.Users.Args.Portfolios;

public sealed record UserPortfolioMediaListArgs(
    Guid UserId,
    Guid TalentId
);