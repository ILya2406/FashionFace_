using System;

namespace FashionFace.Common.Models.Models;

public sealed record UserRenderPipelineUpdatedEventModel(
    Guid RenderPipelineId,
    Guid UserId
);
