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

public sealed class SelectClaimedForRetryStrategyBuilder : ISelectClaimedRetryStrategyBuilder
{
    public OutboxBatchStrategyArgs Build<TEntity>(
        SelectClaimedRetryStrategyBuilderArgs args
    )
        where TEntity : class, IOutbox
    {
        var (batchCount, retryDelayMinutes) = args;

        const string TableName =
            nameof(TEntity);

        var sql =
            string
                .Format(
                    SqlTemplateConstants.SelectClaimedForRetry,
                    TableName
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
                "Status",
                nameof(OutboxStatus.Claimed)
            ),
            new(
                "ProcessingStartedAt",
                dateTimeDeadline
            ),
            new(
                "BatchCount",
                batchCount
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