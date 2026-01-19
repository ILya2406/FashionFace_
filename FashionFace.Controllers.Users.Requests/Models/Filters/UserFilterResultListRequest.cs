using System;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record UserFilterResultListRequest(
    Guid FilterId,
    int Limit = 15
);