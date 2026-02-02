using System;

namespace FashionFace.Common.Models.Models;

public sealed record UserRenderPipelineAttemptUpdatedEventModel(
    Guid RenderPipelineAttemptId,
    Guid UserId
);
