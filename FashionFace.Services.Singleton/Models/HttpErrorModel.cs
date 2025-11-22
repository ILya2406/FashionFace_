namespace FashionFace.Services.Singleton.Models;

public sealed record HttpErrorModel(
    ErrorsContainerModel Error,
    int StatusCode
);