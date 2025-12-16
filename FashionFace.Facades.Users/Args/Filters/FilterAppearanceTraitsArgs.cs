using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record FilterAppearanceTraitsArgs(
    SexType? SexType,
    FaceType? FaceType,
    HairColorType? HairColorType,
    HairType? HairType,
    HairLengthType? HairLengthType,
    BodyType? BodyType,
    SkinToneType? SkinToneType,
    EyeShapeType? EyeShapeType,
    EyeColorType? EyeColorType,
    NoseType? NoseType,
    JawType? JawType,
    int? Height,
    int? ShoeSize,
    FilterMaleTraitsArgs? FilterMaleTraits,
    FilterFemaleTraitsArgs? FilterFemaleTraits
);