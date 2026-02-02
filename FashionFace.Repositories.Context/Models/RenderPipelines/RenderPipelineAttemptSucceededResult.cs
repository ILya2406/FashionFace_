using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.MediaEntities;

namespace FashionFace.Repositories.Context.Models.RenderPipelines;

public sealed class RenderPipelineAttemptSucceededResult : EntityBase, IWithCreatedAt
{
    public required Guid MediaAggregateId { get; set; }
    public required Guid RenderAttemptId { get; set; }

    public required DateTime CreatedAt { get; set; }

    public MediaAggregate? MediaAggregate  { get; set; }
    public RenderPipelineAttempt? RenderAttempt  { get; set; }
}