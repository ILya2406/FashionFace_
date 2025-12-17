using System;

namespace FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;

public sealed record UserFemaleTraitsRequest(
    Guid ProfileId
);