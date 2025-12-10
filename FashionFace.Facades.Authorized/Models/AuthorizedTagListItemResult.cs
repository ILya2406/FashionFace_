using System;

namespace FashionFace.Facades.Authorized.Models;

public sealed record AuthorizedTagListItemResult(
    Guid Id,
    string Name
);