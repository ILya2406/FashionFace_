using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptResultArgs(
    Guid UserId,
    Guid AttemptId
);