using System;

namespace FashionFace.Facades.Users.Args.Portfolios;

public sealed record UserPortfolioMediaPositionUpdateArgs(
    Guid UserId,
    Guid MediaId,
    double PositionIndex
);
