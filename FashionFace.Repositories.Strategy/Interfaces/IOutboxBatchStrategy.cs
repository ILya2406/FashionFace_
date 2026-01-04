using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Strategy.Args;

namespace FashionFace.Repositories.Strategy.Interfaces;

public interface IOutboxBatchStrategy<TEntity>
    where TEntity : class, IOutbox
{
    Task<IReadOnlyList<TEntity>> ClaimBatchAsync(
        OutboxBatchStrategyArgs args
    );

    Task MakeDoneAsync(
        TEntity entity
    );

    Task MakeFailedAsync(
        TEntity entity
    );
}