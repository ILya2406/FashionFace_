using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Files;
using FashionFace.Repositories.Context.Models.RenderPipelines;

namespace FashionFace.Repositories.Context.Models.PoseReferences;

public sealed class PoseReference : EntityBase, IWithIsDeleted, IWithCreatedAt, IWithDescription
{
    public required Guid FileResourceId { get; set; }
    public Guid? ModelFileResourceId { get; set; }
    public required bool IsDeleted { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string Description { get; set; }
    public FileResource? FileResource { get; set; }
    public FileResource? ModelFileResource { get; set; }
    public PoseReferenceMediaAggregate? PoseReferenceMediaAggregate { get; set; }
    public ICollection<PoseReferenceProjection>? PoseReferenceProjectionCollection { get; set; }
}