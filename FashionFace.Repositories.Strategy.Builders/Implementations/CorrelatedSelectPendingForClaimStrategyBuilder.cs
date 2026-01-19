using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Models;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Constants;
using FashionFace.Repositories.Strategy.Builders.Interfaces;

namespace FashionFace.Repositories.Strategy.Builders.Implementations;

public sealed class CorrelatedSelectPendingForClaimStrategyBuilder : ICorrelatedSelectPendingStrategyBuilder
{
    public OutboxBatchStrategyArgs Build<TEntity>(
        CorrelatedSelectPendingStrategyBuilderArgs args
    )
        where TEntity : class, IOutbox
    {
        var (correlationId, batchSize) = args;

        var tableName =
            typeof(TEntity).Name;

        var sql =
            string
                .Format(
                    SqlTemplateConstants.CorrelatedSelectPendingForClaim,
                    tableName
                );

        IReadOnlyList<SqlParameter> parameterList =
        [
            new(
                "OutboxStatus",
                nameof(OutboxStatus.Pending)
            ),
            new(
                "BatchSize",
                batchSize
            ),
            new(
                "CorrelationId",
                correlationId
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