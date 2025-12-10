using System;

namespace FashionFace.Facades.Authorized.Args;

public sealed record AuthorizedTagListArgs(
    Guid UserId
);