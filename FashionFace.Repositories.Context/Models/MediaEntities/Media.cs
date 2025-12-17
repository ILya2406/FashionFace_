using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.MediaEntities;

public sealed class Media : EntityBase,
    IWithIsDeleted
{
    public required Guid OriginalFileId { get; set; }
    public required Guid OptimizedFileId { get; set; }

    public MediaFile? OriginalFile { get; set; }
    public MediaFile? OptimizedFile { get; set; }

    public required bool IsDeleted { get; set; }
}