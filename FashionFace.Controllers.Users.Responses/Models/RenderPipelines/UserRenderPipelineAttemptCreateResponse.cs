using System;

namespace FashionFace.Controllers.Users.Responses.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptCreateResponse(
    Guid AttemptId
);