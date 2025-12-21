using System.Threading.Tasks;

using FashionFace.Common.Models.Models;

using Microsoft.Extensions.Caching.Distributed;

namespace FashionFace.Dependencies.Redis.Interfaces.Base;

public interface IBaseCache<in TKey, TEntity>
    where TEntity : class
{
    Task<ResultContainer<TEntity>> ReadAsync(
        TKey key
    );

    Task SetAsync(
        TKey key,
        TEntity response,
        DistributedCacheEntryOptions? options = null
    );

    Task DeleteAsync(
        TKey key
    );
}