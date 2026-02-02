using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineAttemptCreateArgs(
    Guid UserId,
    Guid RenderPipelineId,
    Guid MediaAggregateId,
    string UserPrompt,
    double Temperature
);