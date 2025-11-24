namespace FashionFace.Controllers.Requests.Models;

public sealed record UserCreateRequest(
    string Email,
    string Username,
    string Password
);