using System.Collections.Generic;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterResultListResponse(
    IReadOnlyList<UserFilterResultListItemResponse> ItemList
);