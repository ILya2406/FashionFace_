using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterMaleTraits : EntityBase
{
    public required Guid FilterAppearanceTraitsId { get; set; }

    public required HairLengthType FacialHairLengthType { get; set; }

    public FilterAppearanceTraits? FilterAppearanceTraits { get; set; }
}