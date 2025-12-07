using System;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class PortfolioTag : EntityBase
{
    public required Guid PortfolioId { get; set; }
    public required Guid TagId { get; set; }

    public required int PositionIndex { get; set; }

    public Portfolio? Portfolio { get; set; }
    public Tag? Tag { get; set; }
}