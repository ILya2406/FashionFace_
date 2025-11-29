namespace FashionFace.Controllers.Requests.Models;

public sealed record RegisterRequest(
    string Username,
    string Password
);