using System;

using FashionFace.Controllers.Users.Responses.Models.Locations;
using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterLocationListItemResponse(
    LocationType LocationType,
    Guid CityId,
    UserPlaceResponse? Place
);