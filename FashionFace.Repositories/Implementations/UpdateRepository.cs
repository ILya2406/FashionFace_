using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Implementations.Base;
using FashionFace.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Implementations;

public sealed class UpdateRepository(
    ApplicationDatabaseContext context
) : Repository(
        context
    ),
    IUpdateRepository
{
    public async Task UpdateAsync<TEntity>(
        TEntity item,
        CancellationToken cancellationToken = default
    )
        where TEntity : class
    {
        void Action(
            DbSet<TEntity> set,
            TEntity entity
        ) =>
            set
                .Update(
                    entity
                );

        await
            InvokeActionAndSaveChangesAsync(
                item,
                Action,
                cancellationToken
            );
    }

    public async Task UpdateCollectionAsync<TEntity>(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken = default
    )
        where TEntity : class
    {
        void Action(
            DbSet<TEntity> set,
            IEnumerable<TEntity> entity
        ) =>
            set
                .UpdateRange(
                    entity
                );

        await
            InvokeActionAndSaveChangesAsync(
                items,
                Action,
                cancellationToken
            );
    }

    public async Task UpdatePropertyAsync<TEntity, TProperty>(
        TEntity entity,
        Expression<Func<TEntity, TProperty>> propertySelector,
        TProperty newValue,
        CancellationToken cancellationToken = default
    )
        where TEntity : class
    {
        void Action(
            DbSet<TEntity> set
        )
        {
            var propertySelectorBody =
                propertySelector.Body;

            var memberExpression =
                (MemberExpression)propertySelectorBody;

            var propertyName =
                memberExpression
                    .Member
                    .Name;

            typeof(TEntity)
                .GetProperty(
                    propertyName
                )!
                .SetValue(
                    entity,
                    newValue
                );
        }

        await
            InvokeActionAndSaveChangesAsync<TEntity>(
                Action,
                cancellationToken
            );
    }
}