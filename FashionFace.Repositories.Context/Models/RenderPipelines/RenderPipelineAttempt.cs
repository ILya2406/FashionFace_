using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;

namespace FashionFace.Repositories.Context.Models.RenderPipelines;

public sealed class RenderPipelineAttempt : EntityBase, IWithCreatedAt, IWithIsDeleted, IWithApplicationUserId
{
    public required Guid ApplicationUserId { get; set; }
    public required Guid RenderPipelineId { get; set; }
    public required Guid RenderAttemptSettingsId { get; set; }

    public required bool IsDeleted { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? StartedAt { get; set; }
    public required DateTime? FinishedAt { get; set; }
    public required PipelineAttemptStatus Status { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public RenderPipeline? RenderPipeline { get; set; }
    public RenderPipelineAttemptSettings? RenderAttemptSettings { get; set; }

    public RenderPipelineAttemptSucceededResult? RenderSucceededResult { get; set; }
    public RenderPipelineAttemptFailedResult? RenderFailedResult { get; set; }
}