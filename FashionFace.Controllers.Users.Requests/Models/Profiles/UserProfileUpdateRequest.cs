using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Profiles;

public sealed record UserProfileUpdateRequest(
    string? Name,
    string? City,
    string? Description,
    AgeCategoryType? AgeCategoryType
);