using System.Collections.Generic;

using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Interfaces;
using FashionFace.Repositories.Models;
using FashionFace.Repositories.Strategy.Args;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Constants;
using FashionFace.Repositories.Strategy.Builders.Interfaces;

namespace FashionFace.Repositories.Strategy.Builders.Implementations;

public sealed class SelectPendingForClaimStrategyBuilder : ISelectPendingStrategyBuilder
{
    public OutboxBatchStrategyArgs Build<TEntity>(
        SelectPendingStrategyBuilderArgs args
    )
        where TEntity : class, IOutbox
    {
        const string TableName =
            nameof(TEntity);

        var sql =
            string
                .Format(
                    SqlTemplateConstants.SelectPendingForClaim,
                    TableName
                );

        var batchCount =
            args.BatchCount;

        IReadOnlyList<SqlParameter> parameterList =
        [
            new(
                "Status",
                nameof(OutboxStatus.Pending)
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