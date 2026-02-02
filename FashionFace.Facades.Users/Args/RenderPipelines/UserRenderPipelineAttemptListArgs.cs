using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptListArgs(
    Guid UserId,
    Guid PipelineId,
    int Offset,
    int Limit
);