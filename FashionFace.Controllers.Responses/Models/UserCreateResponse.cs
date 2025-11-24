using System;

namespace FashionFace.Controllers.Responses.Models;

public sealed record UserCreateResponse(
    Guid UserId
);