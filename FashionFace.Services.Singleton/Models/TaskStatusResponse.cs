namespace FashionFace.Services.Singleton.Models;

public sealed record TaskStatusResponse(
    int Code,
    string? Msg,
    TaskStatusDataResponse? Data
);