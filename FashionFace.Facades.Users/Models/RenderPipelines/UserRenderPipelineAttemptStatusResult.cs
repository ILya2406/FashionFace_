using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptStatusResult(
    PipelineAttemptStatus Status
);