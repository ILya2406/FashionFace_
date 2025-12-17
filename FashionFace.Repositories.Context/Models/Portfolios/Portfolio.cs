using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Talents;

namespace FashionFace.Repositories.Context.Models.Portfolios;

public sealed class Portfolio : EntityBase,
    IWithIsDeleted
{
    public required Guid TalentId { get; set; }
    public required string Description { get; set; }

    public ICollection<PortfolioMediaAggregate> PortfolioMediaCollection { get; set; }
    public ICollection<PortfolioTag> PortfolioTagCollection { get; set; }

    public Talent? Talent { get; set; }

    public required bool IsDeleted { get; set; }
}