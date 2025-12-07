using System.Collections.Generic;

namespace FashionFace.Facades.Base.Models;

public sealed record ListResult<TResult>(
    int TotalCount,
    IReadOnlyList<TResult> ItemList
);