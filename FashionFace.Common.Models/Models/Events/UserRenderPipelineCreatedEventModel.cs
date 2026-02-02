using System;

namespace FashionFace.Common.Models.Models;

public sealed record UserRenderPipelineCreatedEventModel(
    Guid RenderPipelineId,
    Guid UserId
);
