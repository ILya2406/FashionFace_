using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterCriteriaMaleTraits : EntityBase
{
    public required Guid FilterCriteriaAppearanceTraitsId { get; set; }

    public required HairLengthType FacialHairLengthType { get; set; }

    public FilterCriteriaAppearanceTraits? FilterCriteriaAppearanceTraits { get; set; }
}