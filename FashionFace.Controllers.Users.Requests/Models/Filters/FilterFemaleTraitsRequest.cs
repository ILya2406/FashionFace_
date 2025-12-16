using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Controllers.Users.Requests.Models.Filters;

public sealed record FilterFemaleTraitsRequest(
    BustSizeType BustSizeType
);