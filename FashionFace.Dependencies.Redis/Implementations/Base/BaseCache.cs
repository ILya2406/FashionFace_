using System;
using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.Redis.Interfaces.Base;
using FashionFace.Dependencies.Serialization.Interfaces;

using Microsoft.Extensions.Caching.Distributed;

namespace FashionFace.Dependencies.Redis.Implementations.Base;

public abstract class BaseCache<TKey, TEntity>(
    IDistributedCache distributedCache,
    ISerializationDecorator serializationDecorator
) : IBaseCache<TKey, TEntity>
    where TEntity : class
{
    private readonly DistributedCacheEntryOptions
        defaultOptions =
            new()
            {
                SlidingExpiration =
                    TimeSpan
                        .FromMinutes(
                            30
                        ),
            };

    public async Task<ResultContainer<TEntity>> ReadAsync(
        TKey key
    )
    {
        var strKey =
            GetKey(
                key
            );

        var resultJson =
            await
            distributedCache
                .GetStringAsync(
                    strKey
                );

        if (resultJson.IsEmpty())
        {
            var failedResultContainer =
                ResultContainer<TEntity>
                    .Failed(
                        "EmptyResult"
                    );

            return
                failedResultContainer;
        }

        var result =
            serializationDecorator
                .Deserialize<TEntity>(
                    resultJson
                );

        if (result is null)
        {
            var failedResultContainer =
                ResultContainer<TEntity>
                    .Failed(
                        "InvalidResultType"
                    );

            return
                failedResultContainer;
        }

        await
            distributedCache
                .RefreshAsync(
                    strKey
                );

        var successfulResultContainer =
            ResultContainer<TEntity>
                .Successful(
                    result
                );

        return
            successfulResultContainer;
    }

    public async Task SetAsync(
        TKey key,
        TEntity response,
        DistributedCacheEntryOptions? options = null
    )
    {
        var strKey =
            GetKey(
                key
            );

        var responseJson =
            serializationDecorator
                .Serialize(
                    response
                );

        options ??=
            defaultOptions;

        await
            distributedCache
                .SetStringAsync(
                    strKey,
                    responseJson,
                    options
                );
    }

    public async Task DeleteAsync(
        TKey key
    )
    {
        var strKey =
            GetKey(
                key
            );

        await
            distributedCache
                .RemoveAsync(
                    strKey
                );
    }

    protected abstract string GetKey(
        TKey key
    );
}