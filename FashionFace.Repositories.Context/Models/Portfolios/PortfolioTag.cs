using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Tags;

namespace FashionFace.Repositories.Context.Models.Portfolios;

public sealed class PortfolioTag : EntityBase, IWithPositionIndex
{
    public required Guid PortfolioId { get; set; }
    public required Guid TagId { get; set; }

    public required double PositionIndex { get; set; }

    public Portfolio? Portfolio { get; set; }
    public Tag? Tag { get; set; }
}