using System;

namespace FashionFace.Facades.Users.Args;

public sealed record UserPortfolioMediaDeleteArgs(
    Guid UserId,
    Guid MediaId
);