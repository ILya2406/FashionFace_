using System;

namespace FashionFace.Facades.Args;

public sealed record UserCreateArgs(
    Guid UserId,
    string Email,
    string Username,
    string Password
);