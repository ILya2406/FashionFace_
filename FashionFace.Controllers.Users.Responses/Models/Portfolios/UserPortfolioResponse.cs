using System;

namespace FashionFace.Controllers.Users.Responses.Models.Portfolios;

public sealed record UserPortfolioResponse(
    Guid Id,
    Guid TalentId,
    string Description
);