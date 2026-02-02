using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptStatusResponse(
    PipelineAttemptStatus Status
);