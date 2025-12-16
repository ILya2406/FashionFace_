using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Tags;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterTag : EntityBase
{
    public required Guid FilterId { get; set; }
    public required Guid TagId { get; set; }

    public required double PositionIndex { get; set; }

    public Filter? Filter { get; set; }
    public Tag? Tag { get; set; }
}