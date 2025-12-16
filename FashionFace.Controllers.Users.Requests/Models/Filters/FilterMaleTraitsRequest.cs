using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record FilterMaleTraitsRequest(
    HairLengthType FacialHairLengthType
);