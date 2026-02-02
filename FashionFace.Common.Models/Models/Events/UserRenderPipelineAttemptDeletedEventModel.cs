using System;

namespace FashionFace.Common.Models.Models;

public sealed record UserRenderPipelineAttemptDeletedEventModel(
    Guid RenderPipelineAttemptId,
    Guid UserId
);
