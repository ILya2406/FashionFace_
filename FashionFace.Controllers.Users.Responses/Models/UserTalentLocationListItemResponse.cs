using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserTalentLocationListItemResponse(
    Guid Id,
    LocationType Type,
    UserCityResponse City,
    UserPlaceResponse? Place
);