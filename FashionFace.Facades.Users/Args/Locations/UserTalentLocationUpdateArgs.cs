using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.Locations;

public sealed record UserLocationUpdateArgs(
    Guid UserId,
    Guid LocationId,
    LocationType LocationType,
    Guid CityId,
    PlaceArgs? Place
);