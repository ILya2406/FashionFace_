using System;

namespace FashionFace.Facades.Users.Models.RenderPipelines;

public sealed record UserRenderPipelineResult(
    Guid Id,
    Guid TalentId,
    Guid PoseReferenceId,
    Guid? ProductMediaAggregateId,
    string Name,
    string Description
);