using System;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record UserFilterResultStatusRequest(
    Guid FilterId
);