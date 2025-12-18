using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

namespace FashionFace.Repositories.Context.Models.Portfolios;

public sealed class PortfolioMediaAggregate : EntityBase,
    IWithPositionIndex
{
    public required Guid PortfolioId { get; set; }
    public required Guid MediaAggregateId { get; set; }

    public required double PositionIndex { get; set; }

    public Portfolio? Portfolio { get; set; }
    public MediaAggregate? MediaAggregate { get; set; }
}