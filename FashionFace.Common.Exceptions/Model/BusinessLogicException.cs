namespace FashionFace.Common.Exceptions.Model;

[Serializable]
public sealed class BusinessLogicException : Exception
{
    public BusinessLogicException(
        string code,
        IDictionary<string, object?>? data = null
    )
    {
        Code =
            code;

        var dataDictionary =
            data
            ?? new Dictionary<string, object?>();

        Data =
            dataDictionary;
    }

    public string Code { get; init; }
    public IDictionary<string, object?> Data { get; init; }
}