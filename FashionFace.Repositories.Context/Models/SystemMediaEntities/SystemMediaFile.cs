using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Files;

namespace FashionFace.Repositories.Context.Models.SystemMediaEntities;

public sealed class SystemMediaFile : EntityBase
{
    public required Guid FileResourceId { get; set; }

    public FileResource? FileResource { get; set; }
}
