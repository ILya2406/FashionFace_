using System.Text.Json;

namespace FashionFace.Dependencies.Serialization.Interfaces;

public interface ISerializationDecorator
{
    TEntity? Deserialize<TEntity>(
        string json
    );

    TEntity? Deserialize<TEntity>(
        string json,
        JsonSerializerOptions options
    );

    string Serialize<TEntity>(
        TEntity entity
    );

    string Serialize<TEntity>(
        TEntity entity,
        JsonSerializerOptions options
    );

    JsonDocument SerializeToDocument<TEntity>(
        TEntity entity
    );

    ValueTask<TEntity?> DeserializeAsync<TEntity>(
        Stream stream
    );
}