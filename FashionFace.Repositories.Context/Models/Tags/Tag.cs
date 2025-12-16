using System.Collections.Generic;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.Portfolios;

namespace FashionFace.Repositories.Context.Models.Tags;

public sealed class Tag : EntityBase
{
    public required string Name { get; set; }

    public ICollection<MediaAggregateTag> PortfolioMediaTagCollection { get; set; }
    public ICollection<PortfolioTag> PortfolioTagCollection { get; set; }
}