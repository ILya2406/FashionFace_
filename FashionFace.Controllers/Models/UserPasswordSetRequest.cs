namespace FashionFace.Controllers.Models;

public sealed record UserPasswordSetRequest(
    string OldPassword,
    string NewPassword
);