namespace FashionFace.Services.Singleton.Models;

public sealed record TaskResponse(
    string OriginImageUrl,
    string ResultImageUrl
);