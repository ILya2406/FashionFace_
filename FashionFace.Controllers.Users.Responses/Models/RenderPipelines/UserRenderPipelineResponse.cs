using System;

namespace FashionFace.Controllers.Users.Responses.Models.RenderPipelines;

public sealed record UserRenderPipelineResponse(
    Guid Id,
    Guid TalentId,
    Guid PoseReferenceId,
    Guid? ProductMediaAggregateId,
    string Name,
    string Description
);