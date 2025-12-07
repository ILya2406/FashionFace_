using System.Collections.Generic;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class Tag : EntityBase
{
    public required string Name { get; set; }

    public ICollection<PortfolioMediaTag> PortfolioMediaTagCollection { get; set; }
    public ICollection<PortfolioTag> PortfolioTagCollection { get; set; }
}