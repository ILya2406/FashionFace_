namespace FashionFace.Facades.Domains.Args;

public sealed record AuthenticationModelCreateArgs(
    Guid UserId,
    string Email
);