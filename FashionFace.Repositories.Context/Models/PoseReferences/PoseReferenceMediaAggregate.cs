using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

namespace FashionFace.Repositories.Context.Models.PoseReferences;

public sealed class PoseReferenceMediaAggregate : EntityBase
{
    public required Guid PoseReferenceId { get; set; }
    public required Guid MediaAggregateId { get; set; }

    public PoseReference? PoseReference { get; set; }
    public MediaAggregate? MediaAggregate { get; set; }
}