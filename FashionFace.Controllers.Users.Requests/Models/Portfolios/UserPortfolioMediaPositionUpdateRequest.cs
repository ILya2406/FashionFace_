using System;

namespace FashionFace.Controllers.Users.Requests.Models.Portfolios;

public sealed record UserPortfolioMediaPositionUpdateRequest(
    Guid MediaId,
    double PositionIndex
);
