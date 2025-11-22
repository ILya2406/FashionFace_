using System.Text.Json;
using System.Text.Json.Serialization;

using FashionFace.Dependencies.Serialization.Interfaces;

namespace FashionFace.Dependencies.Serialization.Implementations;

public sealed class SerializationDecorator :
    ISerializationDecorator
{
    public TEntity? Deserialize<TEntity>(
        string json
    ) =>
        JsonSerializer
            .Deserialize<TEntity>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive =
                        true,
                }
            );

    public TEntity? Deserialize<TEntity>(
        string json,
        JsonSerializerOptions options
    ) =>
        JsonSerializer
            .Deserialize<TEntity>(
                json,
                options
            );

    public string Serialize<TEntity>(
        TEntity entity
    )
    {
        var jsonSerializerOptions =
            new JsonSerializerOptions
            {
                PropertyNamingPolicy =
                    JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

        jsonSerializerOptions
            .Converters
            .Add(
                new JsonStringEnumConverter()
            );

        var resultJson =
            JsonSerializer
                .Serialize(
                    entity,
                    jsonSerializerOptions
                );

        return
            resultJson;
    }

    public string Serialize<TEntity>(
        TEntity entity,
        JsonSerializerOptions options
    ) =>
        JsonSerializer
            .Serialize(
                entity,
                options
            );

    public JsonDocument SerializeToDocument<TEntity>(
        TEntity entity
    ) =>
        JsonSerializer
            .SerializeToDocument(
                entity
            );

    public ValueTask<TEntity?> DeserializeAsync<TEntity>(
        Stream stream
    ) =>
        JsonSerializer
            .DeserializeAsync<TEntity>(
                stream,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive =
                        true,
                }
            );
}