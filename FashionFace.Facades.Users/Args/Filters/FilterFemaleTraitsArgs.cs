using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Facades.Users.Args.Filters;

public sealed record FilterFemaleTraitsArgs(
    BustSizeType BustSizeType
);