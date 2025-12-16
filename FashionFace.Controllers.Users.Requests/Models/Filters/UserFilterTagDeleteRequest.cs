using System;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record UserFilterTagDeleteRequest(
    Guid FilterId,
    Guid TagId
);