using System;
using System.Collections.Generic;

namespace FashionFace.Facades.Users.Models;

public sealed record UserMediaListItemResult(
    Guid Id,
    string Description,
    string Url,
    IReadOnlyList<Guid> TagIdList
);