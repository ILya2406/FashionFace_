using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Locations;

public sealed record UserLocationCreateRequest(
    Guid TalentId,
    LocationType LocationType,
    Guid CityId,
    PlaceRequest? Place
);