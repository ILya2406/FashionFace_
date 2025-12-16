using System;

namespace FashionFace.Controllers.Users.Requests.Models.Locations;

public sealed record UserLocationListRequest(
    Guid TalentId
);