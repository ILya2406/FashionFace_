using System;

namespace FashionFace.Controllers.Users.Requests.Models.MediaAggregates;

public sealed record UserMediaAggregateDeleteRequest(
    Guid MediaId
);