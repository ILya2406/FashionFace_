namespace FashionFace.Services.Singleton.Models;

public sealed record NanoBananaResult(
    int Code,
    string? Msg,
    NanoBananaData Data
);