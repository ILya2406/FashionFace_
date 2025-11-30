namespace FashionFace.Controllers.Anonymous.Requests.Models;

public sealed record RefreshRequest(
    string RefreshToken
);