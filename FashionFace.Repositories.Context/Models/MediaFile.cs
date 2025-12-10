using System;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class MediaFile : EntityBase, IWithIsDeleted
{
    public required Guid ProfileId { get; set; }
    public required bool IsDeleted { get; set; }
    public required string Uri { get; set; }

    public Profile? Profile { get; set; }
    // reference to PortfolioMedia Original/Optimized File
}