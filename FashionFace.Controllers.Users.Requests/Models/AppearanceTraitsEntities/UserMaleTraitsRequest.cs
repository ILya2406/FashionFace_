using System;

namespace FashionFace.Controllers.Users.Requests.Models.AppearanceTraitsEntities;

public sealed record UserMaleTraitsRequest(
    Guid ProfileId
);