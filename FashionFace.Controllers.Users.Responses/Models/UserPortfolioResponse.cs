using System;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserPortfolioResponse(
    Guid Id,
    string Description
);