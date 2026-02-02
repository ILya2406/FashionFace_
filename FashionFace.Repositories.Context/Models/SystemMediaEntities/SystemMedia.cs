using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.SystemMediaEntities;

public sealed class SystemMedia : EntityBase,
    IWithIsDeleted
{
    public required Guid OriginalFileId { get; set; }
    public required Guid OptimizedFileId { get; set; }

    public SystemMediaFile? OriginalFile { get; set; }
    public SystemMediaFile? OptimizedFile { get; set; }

    public required bool IsDeleted { get; set; }
}
