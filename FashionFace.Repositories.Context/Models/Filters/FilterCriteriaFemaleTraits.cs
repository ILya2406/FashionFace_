using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterCriteriaFemaleTraits : EntityBase
{
    public required Guid FilterCriteriaAppearanceTraitsId { get; set; }

    public required BustSizeType BustSizeType { get; set; }

    public FilterCriteriaAppearanceTraits? FilterCriteriaAppearanceTraits { get; set; }
}