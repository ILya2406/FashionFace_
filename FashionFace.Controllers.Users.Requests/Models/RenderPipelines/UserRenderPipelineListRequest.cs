namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineListRequest(
    int Offset,
    int Limit
);