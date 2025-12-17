using System;

namespace FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;

public sealed record UserAppearanceTraitsRequest(
    Guid ProfileId
);