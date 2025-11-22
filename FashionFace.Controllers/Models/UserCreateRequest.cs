namespace FashionFace.Controllers.Models;

public sealed record UserCreateRequest(
    string Email,
    string Username,
    string Password
);