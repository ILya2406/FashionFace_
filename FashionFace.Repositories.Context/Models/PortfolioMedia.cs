using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class PortfolioMedia : EntityBase, IWithIsDeleted, IWithPositionIndex
{
    public required Guid PortfolioId { get; set; }
    public required Guid OriginalFileId { get; set; }
    public required Guid OptimizedFileId { get; set; }

    public required bool IsDeleted { get; set; }
    public required double PositionIndex { get; set; }
    public required string SystemFileName { get; set; }
    public required string OriginalFileName { get; set; }
    public required string Description { get; set; }

    public ICollection<PortfolioMediaTag> PortfolioMediaTagCollection { get; set; }

    public MediaFile? OriginalFile { get; set; }
    public MediaFile? OptimizedFile { get; set; }
    public Portfolio? Portfolio { get; set; }
}