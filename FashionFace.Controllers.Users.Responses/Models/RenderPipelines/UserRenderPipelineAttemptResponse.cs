namespace FashionFace.Controllers.Users.Responses.Models.RenderPipelines;

public sealed record UserRenderPipelineAttemptResponse(
    string UserPrompt,
    string PoseProjectionRelativePath
);