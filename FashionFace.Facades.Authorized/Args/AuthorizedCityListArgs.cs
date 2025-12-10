using System;

namespace FashionFace.Facades.Authorized.Args;

public sealed record AuthorizedCityListArgs(
    Guid UserId
);