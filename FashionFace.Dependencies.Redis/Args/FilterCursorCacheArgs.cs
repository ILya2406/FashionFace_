using System;

namespace FashionFace.Dependencies.Redis.Args;

public sealed record FilterCursorCacheArgs(
    Guid Id,
    int Version
);