using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineListArgs(
    Guid UserId,
    int Offset,
    int Limit
);