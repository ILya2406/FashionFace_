using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.MediaEntities;

public sealed class MediaAggregate : EntityBase,
    IWithIsDeleted
{
    public required Guid PreviewMediaId { get; set; }
    public required Guid OriginalMediaId { get; set; }
    public required string Description { get; set; }

    public Media? PreviewMedia { get; set; }
    public Media? OriginalMedia { get; set; }

    public ICollection<MediaAggregateTag> PortfolioMediaTagCollection { get; set; }

    public required bool IsDeleted { get; set; }
}