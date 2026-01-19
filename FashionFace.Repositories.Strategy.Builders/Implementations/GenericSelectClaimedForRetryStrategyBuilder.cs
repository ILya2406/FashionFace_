using System;
using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Models;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Constants;
using FashionFace.Repositories.Strategy.Builders.Interfaces;

namespace FashionFace.Repositories.Strategy.Builders.Implementations;

public sealed class GenericSelectClaimedForRetryStrategyBuilder : IGenericSelectClaimedRetryStrategyBuilder
{
    public OutboxBatchStrategyArgs Build<TEntity>(
        GenericSelectClaimedRetryStrategyBuilderArgs args
    )
        where TEntity : class, IOutbox
    {
        var (batchSize, retryDelayMinutes) = args;

        var tableName =
            typeof(TEntity).Name;

        var sql =
            string
                .Format(
                    SqlTemplateConstants.SelectClaimedForRetry,
                    tableName
                );

        var dateTimeDeadline =
            DateTime
                .UtcNow
                .AddMinutes(
                    -retryDelayMinutes
                );

        IReadOnlyList<SqlParameter> parameterList =
        [
            new(
                "OutboxStatus",
                nameof(OutboxStatus.Claimed)
            ),
            new(
                "ClaimedAt",
                dateTimeDeadline
            ),
            new(
                "BatchSize",
                batchSize
            ),
        ];

        var postgresOutboxBatchStrategyArgs =
            new OutboxBatchStrategyArgs(
                sql,
                parameterList
            );

        return
            postgresOutboxBatchStrategyArgs;
    }
}