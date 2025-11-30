using System;

namespace FashionFace.Facades.Admins.Args;

public sealed record UserPasswordSetArgs(
    Guid UserId,
    string OldPassword,
    string NewPassword
);