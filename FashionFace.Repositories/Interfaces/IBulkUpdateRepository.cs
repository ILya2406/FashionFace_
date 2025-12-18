using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Query;

namespace FashionFace.Repositories.Interfaces;

public interface IBulkUpdateRepository
{
    Task<int> ExecuteUpdateAsync<TEntity>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setFunction,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;
}