using System;

namespace FashionFace.Facades.Admins.Args;

public sealed record UserCreateArgs(
    Guid UserId,
    string Email,
    string Username,
    string Password
);