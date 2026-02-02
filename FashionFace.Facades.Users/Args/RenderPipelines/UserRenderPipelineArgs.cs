using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineArgs(
    Guid UserId,
    Guid PipelineId
);