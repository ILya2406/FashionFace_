using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Tags;

namespace FashionFace.Repositories.Context.Models.MediaEntities;

public sealed class MediaAggregateTag : EntityBase,
    IWithPositionIndex
{
    public required Guid MediaAggregateId { get; set; }
    public required Guid TagId { get; set; }

    public MediaAggregate? MediaAggregate { get; set; }
    public Tag? Tag { get; set; }

    public required double PositionIndex { get; set; }
}