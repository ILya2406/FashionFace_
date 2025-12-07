using System;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class PortfolioMediaTag : EntityBase
{
    public required Guid PortfolioMediaId { get; set; }
    public required Guid TagId { get; set; }

    public required int PositionIndex { get; set; }

    public PortfolioMedia? PortfolioMedia { get; set; }
    public Tag? Tag { get; set; }
}