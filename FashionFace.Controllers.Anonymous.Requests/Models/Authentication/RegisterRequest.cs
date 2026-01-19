using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Anonymous.Requests.Models.Authentication;

public sealed record RegisterRequest(
    string Email,
    string Password,
    SexType SexType
);