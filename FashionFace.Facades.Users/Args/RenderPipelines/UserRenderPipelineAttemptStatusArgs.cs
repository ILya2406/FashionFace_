using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptStatusArgs(
    Guid UserId,
    Guid AttemptId
);