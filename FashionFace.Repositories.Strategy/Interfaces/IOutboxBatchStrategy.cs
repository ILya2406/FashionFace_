using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Repositories.Strategy.Args;

namespace FashionFace.Repositories.Strategy.Interfaces;

public interface IOutboxBatchStrategy<TEntity>
    where TEntity : class
{
    Task<IReadOnlyList<TEntity>> ClaimBatchAsync(
        PostgresOutboxBatchStrategyArgs args
    );
}