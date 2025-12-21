using System;

namespace FashionFace.Common.Models.Models;

public sealed record AppearanceTraitsUpdatedEventModel(
    Guid ProfileId
);