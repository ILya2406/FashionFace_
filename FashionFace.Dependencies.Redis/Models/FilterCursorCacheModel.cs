using System;

namespace FashionFace.Dependencies.Redis.Models;

public sealed record FilterCursorCacheModel(
    Guid TalentId,
    Guid FilterId,
    int FilterVersion
);