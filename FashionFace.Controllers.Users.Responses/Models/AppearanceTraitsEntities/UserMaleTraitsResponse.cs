using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.AppearanceTraitsEntities;

public sealed record UserMaleTraitsResponse(
    HairLengthType FacialHairLengthType
);