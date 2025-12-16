using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record FilterLocationRequest(
    Guid CityId,
    LocationType LocationType
);