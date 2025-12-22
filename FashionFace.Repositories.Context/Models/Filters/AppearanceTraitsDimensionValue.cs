using System;

using FashionFace.Repositories.Context.Models.Base;
using FashionFace.Repositories.Context.Models.Profiles;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class AppearanceTraitsDimensionValue : EntityBase
{
    public required Guid ProfileId { get; set; }
    public required Guid DimensionValueId { get; set; }

    public Profile? Profile { get; set; }
    public DimensionValue? DimensionValue { get; set; }
}