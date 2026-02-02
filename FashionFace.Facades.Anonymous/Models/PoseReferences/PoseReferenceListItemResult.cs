using System;

namespace FashionFace.Facades.Anonymous.Models.PoseReferences;

public sealed record PoseReferenceListItemResult(
    Guid Id,
    string Description,
    string PreviewImageUrl,
    string? ModelUrl
);
