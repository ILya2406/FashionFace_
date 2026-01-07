using System;

namespace FashionFace.Controllers.Users.Responses.Models.MediaEntity;

public sealed record UserMediaCreateResponse(
    Guid MediaId,
    string ImageUrl
);