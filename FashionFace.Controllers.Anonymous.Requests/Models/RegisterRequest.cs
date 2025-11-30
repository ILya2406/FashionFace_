namespace FashionFace.Controllers.Anonymous.Requests.Models;

public sealed record RegisterRequest(
    string Email,
    string Password
);