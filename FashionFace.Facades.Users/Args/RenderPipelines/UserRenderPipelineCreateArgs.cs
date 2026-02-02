using System;

namespace FashionFace.Facades.Users.Args.RenderPipelines;

public sealed record UserRenderPipelineCreateArgs(
    Guid UserId,
    Guid TalentId,
    Guid PoseReferenceId,
    Guid? ProductMediaAggregateId,
    string Name
);