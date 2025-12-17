using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.AppearanceTraitsEntities;

public sealed record UserFemaleTraitsUpdateArgs(
    Guid UserId,
    BustSizeType BustSizeType
);