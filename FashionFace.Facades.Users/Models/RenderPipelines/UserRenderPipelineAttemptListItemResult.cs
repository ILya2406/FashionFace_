using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptListItemResult(
    Guid Id,
    PipelineAttemptStatus Status
);