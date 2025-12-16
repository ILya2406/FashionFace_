using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.Locations;

public sealed record UserLocationCreateArgs(
    Guid UserId,
    Guid TalentId,
    LocationType LocationType,
    Guid CityId,
    PlaceArgs? Place
);