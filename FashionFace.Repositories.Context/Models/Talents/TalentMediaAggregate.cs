using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

namespace FashionFace.Repositories.Context.Models.Talents;

public sealed class TalentMediaAggregate : EntityBase
{
    public required Guid TalentId { get; set; }
    public required Guid MediaAggregateId { get; set; }

    public Talent? Talent { get; set; }
    public MediaAggregate? MediaAggregate { get; set; }
}