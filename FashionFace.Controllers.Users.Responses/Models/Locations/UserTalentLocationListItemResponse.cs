using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.Locations;

public sealed record UserLocationListItemResponse(
    Guid Id,
    LocationType Type,
    UserCityResponse City,
    UserPlaceResponse? Place
);