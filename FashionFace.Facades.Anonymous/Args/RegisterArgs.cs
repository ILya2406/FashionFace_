using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Anonymous.Args;

public sealed record RegisterArgs(
    string Email,
    string Password,
    SexType SexType
);