namespace FashionFace.Services.Singleton.Args;

public sealed record GenerateImageArgs(
    string ApiKey,
    string Prompt,
    string? Type = null,
    int? NumImages = null,
    string? CallBackUrl = null,
    bool? Watermark = null,
    string[]? ImageUrls = null
);