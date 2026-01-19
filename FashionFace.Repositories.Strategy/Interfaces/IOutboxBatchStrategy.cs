using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Strategy.Args;

namespace FashionFace.Repositories.Strategy.Interfaces;

public interface IOutboxBatchStrategy
{
    Task<IReadOnlyList<TEntity>> ClaimBatchAsync<TEntity>(
        OutboxBatchStrategyArgs args
    ) where TEntity : class, IOutbox;

    Task MakeDoneAsync<TEntity>(
        TEntity entity
    ) where TEntity : class, IOutbox;

    Task MakeFailedAsync<TEntity>(
        TEntity entity
    ) where TEntity : class, IOutbox;
}