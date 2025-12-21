using System.Collections.Generic;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterResultListResult(
    IReadOnlyList<UserFilterResultListItemResult> ItemList
);