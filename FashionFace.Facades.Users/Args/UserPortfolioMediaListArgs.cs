using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserPortfolioMediaListArgs(
    Guid UserId,
    Guid PortfolioId
);