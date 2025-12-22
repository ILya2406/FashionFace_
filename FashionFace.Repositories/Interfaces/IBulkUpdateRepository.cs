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
        Action<UpdateSettersBuilder<TEntity>> setPropertyCalls,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;
}