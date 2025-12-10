using System;

namespace FashionFace.Facades.Authorized.Models;

public sealed record AuthorizedCityListItemResult(
    Guid Id,
    string Country,
    string Name
);