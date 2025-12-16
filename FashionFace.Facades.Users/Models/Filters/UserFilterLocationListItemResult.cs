using System;

using FashionFace.Facades.Users.Models.Locations;
using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterLocationListItemResult(
    LocationType LocationType,
    Guid CityId,
    PlaceModel? Place
);