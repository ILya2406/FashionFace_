using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Builders.Args;

namespace FashionFace.Repositories.Strategy.Builders.Interfaces;

public interface IGenericSelectClaimedRetryStrategyBuilder
{
    OutboxBatchStrategyArgs Build<TEntity>(
        GenericSelectClaimedRetryStrategyBuilderArgs args
    )
        where TEntity : class, IOutbox;
}