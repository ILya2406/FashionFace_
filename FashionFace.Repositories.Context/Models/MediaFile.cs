using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class MediaFile : EntityBase
{
    public required string Uri { get; set; }

    // reference to PortfolioMedia Original/Optimized File
}