using System;

namespace FashionFace.Facades.Users.Models;

public sealed record UserPortfolioResult(
    Guid Id,
    string Description
);