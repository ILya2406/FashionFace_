using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Tags;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterCriteriaTag : EntityBase
{
    public required Guid FilterCriteriaId { get; set; }
    public required Guid TagId { get; set; }

    public required double PositionIndex { get; set; }

    public FilterCriteria? FilterCriteria { get; set; }
    public Tag? Tag { get; set; }
}