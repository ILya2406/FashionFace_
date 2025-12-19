using System;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.Base;

namespace FashionFace.Repositories.Context.Models.Filters;

public sealed class FilterCriteriaAppearanceTraits : EntityBase
{
    public required Guid FilterCriteriaId { get; set; }

    public SexType? SexType { get; set; }
    public FaceType? FaceType { get; set; }
    public HairColorType? HairColorType { get; set; }
    public HairType? HairType { get; set; }
    public HairLengthType? HairLengthType { get; set; }
    public BodyType? BodyType { get; set; }
    public SkinToneType? SkinToneType { get; set; }
    public EyeShapeType? EyeShapeType { get; set; }
    public EyeColorType? EyeColorType { get; set; }
    public NoseType? NoseType { get; set; }
    public JawType? JawType { get; set; }
    public int? Height { get; set; }
    public int? ShoeSize { get; set; }
    public FilterCriteriaMaleTraits? FilterMaleTraits { get; set; }
    public FilterCriteriaFemaleTraits? FilterFemaleTraits { get; set; }

    public FilterCriteria? FilterCriteria { get; set; }
}