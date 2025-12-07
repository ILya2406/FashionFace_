using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models;

public sealed class MaleTraits : EntityBase
{
    public required Guid AppearanceTraitsId { get; set; }

    public HairLengthType FacialHairLengthType { get; set; }

    public AppearanceTraits? AppearanceTraits { get; set; }
}