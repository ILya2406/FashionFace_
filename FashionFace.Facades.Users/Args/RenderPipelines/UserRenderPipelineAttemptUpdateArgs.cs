using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptUpdateArgs(
    Guid UserId,
    Guid AttemptId,
    string UserPrompt
);