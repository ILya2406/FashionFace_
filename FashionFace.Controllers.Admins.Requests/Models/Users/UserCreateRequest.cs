namespace FashionFace.Controllers.Admins.Requests.Models.Users;

public sealed record UserCreateRequest(
    string Email,
    string Username,
    string Password
);