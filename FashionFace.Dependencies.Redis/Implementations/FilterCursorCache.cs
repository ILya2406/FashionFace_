using FashionFace.Dependencies.Redis.Args;
using FashionFace.Dependencies.Redis.Implementations.Base;
using FashionFace.Dependencies.Redis.Interfaces;
using FashionFace.Dependencies.Redis.Models;
using FashionFace.Dependencies.Serialization.Interfaces;

using Microsoft.Extensions.Caching.Distributed;

namespace FashionFace.Dependencies.Redis.Implementations;

public sealed class FilterCursorCache(
    IDistributedCache distributedCache,
    ISerializationDecorator serializationDecorator
) : BaseCache<FilterCursorCacheArgs, FilterCursorCacheModel>(
        distributedCache,
        serializationDecorator
    ),
    IFilterCursorCache
{
    protected override string GetKey(FilterCursorCacheArgs key) =>
        $"filter_{key.Id}:{key.Version}";
}