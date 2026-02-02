using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.RenderPipelines;

public sealed class RenderPipelineAttemptFailedResult : EntityBase, IWithCreatedAt, IWithDescription
{
    public required Guid RenderAttemptId { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string Description { get; set; }

    public RenderPipelineAttempt? RenderAttempt  { get; set; }
}