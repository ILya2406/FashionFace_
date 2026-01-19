using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Strategy.Implementations;

public sealed class OutboxBatchStrategy(
    IExecuteRepository executeRepository,
    IUpdateRepository updateRepository,
    ITransactionManager transactionManager,
    IDateTimePicker dateTimePicker
) : IOutboxBatchStrategy
{
    public async Task<IReadOnlyList<TEntity>> ClaimBatchAsync<TEntity>(
        OutboxBatchStrategyArgs args
    ) where TEntity : class, IOutbox
    {
        var (sql, parameterList) = args;

        using var transaction =
            await
                transactionManager.BeginTransaction();

        var entityList =
            await
                executeRepository
                    .FromSqlRaw<TEntity>(
                        sql,
                        parameterList
                    )
                    .ToListAsync();

        foreach (var entity in entityList)
        {
            entity.AttemptCount++;
            entity.OutboxStatus = OutboxStatus.Claimed;
            entity.ClaimedAt = dateTimePicker.GetUtcNow();
        }

        await
            updateRepository
                .UpdateCollectionAsync(
                    entityList
                );

        await
            transaction.CommitAsync();

        return
            entityList;
    }

    public async Task MakeDoneAsync<TEntity>(
        TEntity entity
    ) where TEntity : class, IOutbox =>
        await
            SetOutboxStatusAsync(
                entity,
                OutboxStatus.Done
            );

    public async Task MakeFailedAsync<TEntity>(
        TEntity entity
    ) where TEntity : class, IOutbox =>
        await
            SetOutboxStatusAsync(
                entity,
                OutboxStatus.Failed
            );

    private async Task SetOutboxStatusAsync<TEntity>(
        TEntity entity,
        OutboxStatus outboxStatus
    ) where TEntity : class, IOutbox
    {
        entity.OutboxStatus = outboxStatus;

        await
            updateRepository
                .UpdateAsync(
                    entity
                );
    }
}