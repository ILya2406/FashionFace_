using System;

namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptCreateRequest(
    Guid RenderPipelineId,
    Guid PoseReferenceMediaAggregateId,
    string UserPrompt,
    double Temperature
);