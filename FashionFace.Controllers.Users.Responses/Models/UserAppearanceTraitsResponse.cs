using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserAppearanceTraitsResponse(
    SexType SexType,
    FaceType FaceType,
    HairColorType HairColorType,
    HairType HairType,
    HairLengthType HairLengthType,
    BodyType BodyType,
    SkinToneType SkinToneType,
    EyeShapeType EyeShapeType,
    EyeColorType EyeColorType,
    NoseType NoseType,
    JawType JawType,
    int Height,
    int ShoeSize
);