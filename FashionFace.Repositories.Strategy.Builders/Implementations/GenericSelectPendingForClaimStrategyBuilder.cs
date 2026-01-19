using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Models;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Constants;
using FashionFace.Repositories.Strategy.Builders.Interfaces;

namespace FashionFace.Repositories.Strategy.Builders.Implementations;

public sealed class GenericSelectPendingForClaimStrategyBuilder : IGenericSelectPendingStrategyBuilder
{
    public OutboxBatchStrategyArgs Build<TEntity>(
        GenericSelectPendingStrategyBuilderArgs args
    )
        where TEntity : class, IOutbox
    {
        var tableName =
            typeof(TEntity).Name;

        var sql =
            string
                .Format(
                    SqlTemplateConstants.SelectPendingForClaim,
                    tableName
                );

        var batchSize =
            args.BatchSize;

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