using System;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record UserFilterCursorDeleteRequest(
    Guid FilterId
);