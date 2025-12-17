using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Profiles;

public sealed record UserProfileUpdateRequest(
    string? Description,
    AgeCategoryType? AgeCategoryType
);