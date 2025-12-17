using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.AppearanceTraitsEntities;

public sealed record UserAppearanceTraitsUpdateArgs(
    Guid UserId,
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
    int? ShoeSize
);