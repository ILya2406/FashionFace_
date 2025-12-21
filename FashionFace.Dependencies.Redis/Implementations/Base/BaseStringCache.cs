using FashionFace.Dependencies.Serialization.Interfaces;

using Microsoft.Extensions.Caching.Distributed;

namespace FashionFace.Dependencies.Redis.Implementations.Base;

public abstract class BaseStringCache<TEntity>(
    IDistributedCache distributedCache,
    ISerializationDecorator serializationDecorator
) : BaseCache<string, TEntity>(
        distributedCache,
        serializationDecorator
    )
    where TEntity : class
{
    protected override string GetKey(
        string key
    ) => key;
}