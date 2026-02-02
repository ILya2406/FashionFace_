using System;

namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptListRequest(
    Guid PipelineId,
    int Offset,
    int Limit
);