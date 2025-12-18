using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Models.Filters;

public sealed record UserFilterResultStatusResult(
    FilterResultStatus Status,
    int TalentCollectionCount
);