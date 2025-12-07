using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Responses.Models;

public sealed record UserFemaleTraitsResponse(
    BustSizeType BustSizeType
);