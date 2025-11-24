namespace FashionFace.Controllers.Requests.Models;

public sealed record LoginRequest(
    string Username,
    string Password
);