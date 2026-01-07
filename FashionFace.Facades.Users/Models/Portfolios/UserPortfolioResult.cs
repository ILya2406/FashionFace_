using System;

namespace FashionFace.Facades.Users.Models.Portfolios;

public sealed record UserPortfolioResult(
    Guid Id,
    Guid TalentId,
    string Description
);