using System;

namespace FashionFace.Facades.Users.Models.RenderPipelines;

public sealed record UserRenderPipelineListItemResult(
    Guid Id,
    string Name
);