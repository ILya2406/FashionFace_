using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace FashionFace.Repositories.Implementations;

public sealed class BulkUpdateRepository(
    ApplicationDatabaseContext context
) : IBulkUpdateRepository
{
    public Task<int> ExecuteUpdateAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setFunction,
        CancellationToken cancellationToken = default
    )
        where TEntity : class =>
        context
            .Set<TEntity>()
            .Where(
                predicate
            )
            .ExecuteUpdateAsync(
                setFunction,
                cancellationToken
            );
}