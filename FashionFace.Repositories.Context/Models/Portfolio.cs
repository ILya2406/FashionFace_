using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class Portfolio : EntityBase
{
    public required Guid TalentId { get; set; }

    public required string Description { get; set; }

    public ICollection<PortfolioMedia> PortfolioMediaCollection { get; set; }
    public ICollection<PortfolioTag> PortfolioTagCollection { get; set; }

    public Talent? Talent { get; set; }
}