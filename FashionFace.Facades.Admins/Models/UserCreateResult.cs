using System;

namespace FashionFace.Facades.Admins.Models;

public sealed record UserCreateResult(
    Guid UserId
);