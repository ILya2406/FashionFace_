namespace FashionFace.Controllers.Anonymous.Responses.Models.Profile;

public sealed record AppearanceTraitsResponse(
    int? Height,
    int? ShoeSize,
    string? HairColor,
    string? HairType,
    string? HairLength,
    string? EyeColor,
    string? EyeShape,
    string? SkinTone,
    string? BodyType,
    string? FaceType,
    string? NoseType,
    string? JawType
);
