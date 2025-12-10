using System;

namespace FashionFace.Controllers.Authorized.Responses.Models;

public sealed record AuthorizedTagListItemResponse(
    Guid Id,
    string Name
);