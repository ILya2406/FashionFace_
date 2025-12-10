using System;

namespace FashionFace.Controllers.Authorized.Responses.Models;

public sealed record AuthorizedCityListItemResponse(
    Guid Id,
    string Country,
    string Name
);