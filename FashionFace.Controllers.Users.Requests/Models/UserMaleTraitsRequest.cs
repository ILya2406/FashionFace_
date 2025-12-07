using System;

namespace FashionFace.Controllers.Users.Requests.Models;

public sealed record UserMaleTraitsRequest(
    Guid ProfileId
);