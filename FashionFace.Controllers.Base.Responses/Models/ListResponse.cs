using System.Collections.Generic;

namespace FashionFace.Controllers.Base.Responses.Models;

public sealed record ListResponse<TResponse>(
    int TotalCount,
    IReadOnlyList<TResponse> ItemList
);