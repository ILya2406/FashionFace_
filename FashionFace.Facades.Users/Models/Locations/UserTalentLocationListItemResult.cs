using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.Locations;

public sealed record UserLocationListItemResult(
    Guid Id,
    LocationType Type,
    CityModel City,
    PlaceModel? Place
);