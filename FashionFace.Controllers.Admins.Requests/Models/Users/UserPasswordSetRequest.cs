namespace FashionFace.Controllers.Admins.Requests.Models.Users;

public sealed record UserPasswordSetRequest(
    string OldPassword,
    string NewPassword
);