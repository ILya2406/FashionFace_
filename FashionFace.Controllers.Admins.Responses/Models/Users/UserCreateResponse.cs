using System;

namespace FashionFace.Controllers.Admins.Responses.Models.Users;

public sealed record UserCreateResponse(
    Guid UserId
);