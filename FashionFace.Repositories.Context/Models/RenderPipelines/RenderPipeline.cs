using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.IdentityEntities;
using FashionFace.Repositories.Context.Models.MediaEntities;
using FashionFace.Repositories.Context.Models.PoseReferences;
using FashionFace.Repositories.Context.Models.Talents;

namespace FashionFace.Repositories.Context.Models.RenderPipelines;

public sealed class RenderPipeline : EntityBase, IWithCreatedAt, IWithName, IWithDescription, IWithIsDeleted, IWithApplicationUserId
{
    public required Guid ApplicationUserId { get; set; }
    public required Guid TalentId { get; set; }
    public required Guid PoseReferenceId { get; set; }
    public Guid? ProductMediaAggregateId { get; set; }

    public required bool IsDeleted { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }
    public Talent? Talent { get; set; }
    public PoseReference? PoseReference { get; set; }
    public MediaAggregate? ProductMediaAggregate { get; set; }
}