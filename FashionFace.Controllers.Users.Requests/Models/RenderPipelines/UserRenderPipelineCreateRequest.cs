using System;

namespace FashionFace.Controllers.Users.Requests.Models.RenderPipelines;

public sealed record UserRenderPipelineCreateRequest(
    Guid TalentId,
    Guid PoseReferenceId,
    Guid? ProductMediaAggregateId,
    string Name
);