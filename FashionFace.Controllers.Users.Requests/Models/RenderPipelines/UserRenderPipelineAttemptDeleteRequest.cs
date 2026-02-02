using System;

namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptDeleteRequest(
    Guid AttemptId
);