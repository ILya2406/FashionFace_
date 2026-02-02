using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.RenderPipelines;

namespace FashionFace.Repositories.Context.Models.TaskEntity;

public sealed class RenderPipelineAttemptCreateRequestTask : EntityBase, ITask
{
    public required Guid InitiatorUserId { get; set; }
    public required Guid RenderPipelineAttemptId { get; set; }
    public required Guid ModelMediaAggregateId { get; set; }
    public Guid? ProductMediaAggregateId { get; set; }
    public required Guid PoseReferenceMediaAggregateId { get; set; }
    public required string UserPrompt { get; set; }
    public required double Temperature { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required Guid CorrelationId { get; set; }
    public required TaskStatus TaskStatus { get; set; }
    public required int AttemptCount { get; set; }
    public required DateTime? ClaimedAt { get; set; }

    public ApplicationUser? InitiatorUser  { get; set; }
    public RenderPipelineAttempt? RenderPipelineAttempt  { get; set; }
    public MediaAggregate? ModelMediaAggregate { get; set; }
    public MediaAggregate? ProductMediaAggregate { get; set; }
    public MediaAggregate? PoseReferenceMediaAggregate { get; set; }
}