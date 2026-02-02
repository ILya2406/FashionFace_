using System;

namespace FashionFace.Facades.Users.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptCreateResult(
    Guid AttemptId
);