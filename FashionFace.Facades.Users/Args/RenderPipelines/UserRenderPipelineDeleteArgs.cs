using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineDeleteArgs(
    Guid UserId,
    Guid PipelineId
);