using System;

namespace FashionFace.Controllers.Users.Requests.Models;

public sealed record UserFemaleTraitsRequest(
    Guid ProfileId
);