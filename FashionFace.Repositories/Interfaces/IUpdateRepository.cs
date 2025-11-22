using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Repositories.Interfaces.Base;

namespace FashionFace.Repositories.Interfaces;

public interface IUpdateRepository : IRepository
{
    Task UpdateAsync<TEntity>(
        TEntity item,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;

    Task UpdateCollectionAsync<TEntity>(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;

    Task UpdatePropertyAsync<TEntity, TProperty>(
        TEntity entity,
        Expression<Func<TEntity, TProperty>> propertySelector,
        TProperty newValue,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;
}