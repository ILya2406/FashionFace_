using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Locations;

public sealed record UserLocationUpdateRequest(
    Guid LocationId,
    LocationType LocationType,
    Guid CityId,
    PlaceRequest? Place
);