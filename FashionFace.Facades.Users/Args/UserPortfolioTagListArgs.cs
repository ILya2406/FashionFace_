using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserPortfolioTagListArgs(
    Guid UserId,
    Guid PortfolioId
);