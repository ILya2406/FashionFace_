using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models.Filters;

public sealed record UserFilterResultStatusResponse(
    FilterResultStatus Status,
    int TalentCollectionCount
);