using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Models;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Strategy.Implementations;

public sealed class PostgresOutboxBatchStrategy<TEntity>(
    IExecuteRepository executeRepository,
    IUpdateRepository updateRepository
) : IOutboxBatchStrategy<TEntity>
    where TEntity : class
{
    public async Task<IReadOnlyList<TEntity>> ClaimBatchAsync(
        PostgresOutboxBatchStrategyArgs args
    )
    {
        var (status, batchSize) = args;

        const string SqlFormat =
            """
                SELECT *
                FROM "{0}"
                WHERE "Status" = @Status
                ORDER BY "MessageCreatedAt"
                FOR UPDATE SKIP LOCKED
                LIMIT @BatchCount
            """;

        var tableName =
            typeof(TEntity).Name;

        var sql =
            string
                .Format(
                    SqlFormat,
                    tableName
                );

        IReadOnlyList<SqlParameter> parameterList =
        [
            new SqlParameter(
                "Status",
                status.ToString()
            ),
            new SqlParameter(
                "BatchCount",
                batchSize
            ),
        ];

        var entityList =
            await
                executeRepository
                    .FromSqlRaw<TEntity>(
                        sql,
                        parameterList
                    )
                    .ToListAsync();

        foreach (dynamic entity in entityList)
        {
            entity.AttemptCount++;
            entity.Status = OutboxStatus.Claimed;
            entity.ProcessingStartedAt = DateTime.UtcNow;
        }

        await
            updateRepository
                .UpdateCollectionAsync(
                    entityList
                );

        return
            entityList;
    }
}