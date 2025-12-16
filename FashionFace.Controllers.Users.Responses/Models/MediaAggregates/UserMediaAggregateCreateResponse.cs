using System;

namespace FashionFace.Controllers.Users.Responses.Models.MediaAggregates;

public sealed record UserMediaAggregateCreateResponse(
    Guid MediaId
);