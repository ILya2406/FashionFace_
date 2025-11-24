namespace FashionFace.Controllers.Requests.Models;

public sealed record UserPasswordSetRequest(
    string OldPassword,
    string NewPassword
);