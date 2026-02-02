using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Context.Models.RenderPipelines;

namespace FashionFace.Repositories.Context.Models.TaskEntity;

public sealed class RenderPipelineAttemptCreateTask : EntityBase, ITask
{
    public required Guid InitiatorUserId { get; set; }
    public required Guid RenderPipelineAttemptId { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required Guid CorrelationId { get; set; }
    public required TaskStatus TaskStatus { get; set; }
    public required int AttemptCount { get; set; }
    public required DateTime? ClaimedAt { get; set; }

    public ApplicationUser? InitiatorUser  { get; set; }
    public RenderPipelineAttempt? RenderPipelineAttempt  { get; set; }
}