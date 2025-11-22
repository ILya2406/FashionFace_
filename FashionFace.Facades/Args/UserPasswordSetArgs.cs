using System;

namespace FashionFace.Facades.Args;

public sealed record UserPasswordSetArgs(
    Guid UserId,
    string OldPassword,
    string NewPassword
);