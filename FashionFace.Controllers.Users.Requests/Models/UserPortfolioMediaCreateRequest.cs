using System;

namespace FashionFace.Controllers.Users.Requests.Models;

public sealed record UserPortfolioMediaCreateRequest(
    Guid MediaId,
    Guid PortfolioId
);