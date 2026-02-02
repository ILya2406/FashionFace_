using System;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.RenderPipelines;

public sealed class RenderPipelineAttemptSettings : EntityBase
{
    public required Guid PoseReferenceProjectionId { get; set; }

    public required string UserPrompt { get; set; }
    public required int UserPromptHash { get; set; }
    public required double Temperature { get; set; }

    public PoseReferenceProjection? PoseReferenceProjection { get; set; }
}