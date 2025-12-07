using System;
using System.Collections.Generic;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserMediaListItemResponse(
    Guid Id,
    string Description,
    string Url,
    IReadOnlyList<Guid> TagIdList
);