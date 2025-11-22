using System;

namespace FashionFace.Controllers.Models;

public sealed record UserCreateResponse(
    Guid UserId
);