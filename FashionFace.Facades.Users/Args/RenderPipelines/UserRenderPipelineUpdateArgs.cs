using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineUpdateArgs(
    Guid UserId,
    Guid PipelineId,
    string? Name,
    string? Description
);