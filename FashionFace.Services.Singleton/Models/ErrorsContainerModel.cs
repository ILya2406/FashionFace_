namespace FashionFace.Services.Singleton.Models;

public sealed record ErrorsContainerModel(
    string TraceId,
    ErrorModel Error
);