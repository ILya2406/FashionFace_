namespace FashionFace.Controllers.Models;

public sealed record LoginRequest(
    string Username,
    string Password
);