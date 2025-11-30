namespace FashionFace.Controllers.Anonymous.Requests.Models.Authentication;

public sealed record LoginRequest(
    string Username,
    string Password
);