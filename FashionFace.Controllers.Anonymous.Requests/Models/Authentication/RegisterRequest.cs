namespace FashionFace.Controllers.Anonymous.Requests.Models.Authentication;

public sealed record RegisterRequest(
    string Email,
    string Password
);