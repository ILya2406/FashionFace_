using System;

namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineUpdateRequest(
    Guid PipelineId,
    string? Name,
    string? Description
);