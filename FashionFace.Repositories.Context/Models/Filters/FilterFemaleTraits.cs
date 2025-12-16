using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterFemaleTraits : EntityBase
{
    public required Guid FilterAppearanceTraitsId { get; set; }

    public required BustSizeType BustSizeType { get; set; }

    public FilterAppearanceTraits? FilterAppearanceTraits { get; set; }
}