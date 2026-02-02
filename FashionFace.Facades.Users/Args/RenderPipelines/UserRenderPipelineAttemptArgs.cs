using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptArgs(
    Guid UserId,
    Guid AttemptId
);