using System;

namespace FashionFace.Facades.Users.Models.MediaEntity;

public sealed record UserMediaCreateResult(
    Guid MediaId,
    string ImageUrl
);