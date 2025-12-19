using System;

using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterTemplate : EntityBase
{
    public required Guid FilterCriteriaId { get; set; }

    public FilterResult? FilterResult { get; set; }
    public FilterCriteria? FilterCriteria { get; set; }
}