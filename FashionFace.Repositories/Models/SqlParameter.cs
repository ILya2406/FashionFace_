namespace FashionFace.Repositories.Models;

public sealed record SqlParameter(
    string ParameterName,
    object? Value
);