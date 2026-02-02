using System;

namespace FashionFace.Controllers.Users.Responses.Models.RenderPipelines;

public sealed record UserRenderPipelineListItemResponse(
    Guid Id,
    string Name
);