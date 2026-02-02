using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Files;
using FashionFace.Repositories.Context.Models.Profiles;

namespace FashionFace.Repositories.Context.Models.MediaEntities;

public sealed class MediaFile : EntityBase
{
    public required Guid ProfileId { get; set; }

    public required Guid FileResourceId { get; set; }

    public Profile? Profile { get; set; }
    public FileResource? FileResource { get; set; }
}