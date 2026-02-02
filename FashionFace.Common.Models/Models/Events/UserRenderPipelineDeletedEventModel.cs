using System;

namespace FashionFace.Common.Models.Models;

public sealed record UserRenderPipelineDeletedEventModel(
    Guid RenderPipelineId,
    Guid UserId
);
