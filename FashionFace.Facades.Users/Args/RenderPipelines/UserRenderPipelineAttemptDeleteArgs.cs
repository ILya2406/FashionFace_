using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptDeleteArgs(
    Guid UserId,
    Guid AttemptId
);