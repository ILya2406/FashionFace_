using System;

namespace FashionFace.Facades.Users.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptResult(
    Guid Id,
    string UserPrompt,
    string PoseProjectionRelativePath
);