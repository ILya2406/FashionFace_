namespace FashionFace.Controllers.Anonymous.Requests.Models.Authentication;

public sealed record RefreshRequest(
    string RefreshToken
);