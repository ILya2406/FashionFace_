using System;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record UserFilterTagCreateRequest(
    Guid FilterId,
    Guid TagId
);