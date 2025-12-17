using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;

public sealed record UserMaleTraitsUpdateRequest(
    HairLengthType FacialHairLengthType
);