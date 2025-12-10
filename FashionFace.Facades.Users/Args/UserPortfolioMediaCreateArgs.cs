using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserPortfolioMediaCreateArgs(
    Guid UserId,
    Guid MediaId,
    Guid PortfolioId
);