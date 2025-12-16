using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record FilterLocationArgs(
    Guid CityId,
    LocationType LocationType
);