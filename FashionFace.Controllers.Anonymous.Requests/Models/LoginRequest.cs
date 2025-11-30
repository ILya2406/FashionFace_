namespace FashionFace.Controllers.Anonymous.Requests.Models;

public sealed record LoginRequest(
    string Username,
    string Password
);