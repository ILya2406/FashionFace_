using System;

namespace FashionFace.Controllers.Anonymous.Responses.Models.PoseReferences;

public sealed record PoseReferenceListItemResponse(
    Guid Id,
    string Description,
    string PreviewImageUrl,
    string? ModelUrl
);
