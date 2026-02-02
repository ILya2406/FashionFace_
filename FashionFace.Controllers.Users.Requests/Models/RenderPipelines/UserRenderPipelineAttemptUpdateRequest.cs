using System;

namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptUpdateRequest(
    Guid AttemptId,
    string UserPrompt
);