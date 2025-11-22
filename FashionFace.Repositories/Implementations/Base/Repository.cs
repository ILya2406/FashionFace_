using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Interfaces.Base;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Implementations.Base;

public abstract class Repository(
    ApplicationDatabaseContext context
) : IRepository
{
    protected async Task<int> InvokeActionAndSaveChangesAsync<TEntity>(
        Action<DbSet<TEntity>> action,
        CancellationToken cancellationToken = default
    )
        where TEntity : class
    {
        InvokeActionAsync(
            action
        );

        return
            await SaveChangesAsync(
                cancellationToken
            );
    }

    protected async Task<int> InvokeActionAndSaveChangesAsync<TEntity>(
        TEntity item,
        Action<DbSet<TEntity>, TEntity> action,
        CancellationToken cancellationToken = default
    )
        where TEntity : class
    {
        InvokeActionAsync(
            item,
            action
        );

        return
            await SaveChangesAsync(
                cancellationToken
            );
    }

    protected async Task<int> InvokeActionAndSaveChangesAsync<TEntity>(
        IEnumerable<TEntity> items,
        Action<DbSet<TEntity>, IEnumerable<TEntity>> action,
        CancellationToken cancellationToken = default
    )
        where TEntity : class
    {
        InvokeActionAsync(
            items,
            action
        );

        return
            await SaveChangesAsync(
                cancellationToken
            );
    }

    private void InvokeActionAsync<TEntity>(
        Action<DbSet<TEntity>> action
    )
        where TEntity : class
    {
        var entitySet =
            context.Set<TEntity>();

        action(
            entitySet
        );
    }

    private void InvokeActionAsync<TEntity>(
        TEntity entity,
        Action<DbSet<TEntity>, TEntity> action
    )
        where TEntity : class
    {
        var entitySet =
            context
                .Set<TEntity>();

        action(
            entitySet,
            entity
        );
    }

    private void InvokeActionAsync<TEntity>(
        IEnumerable<TEntity> entities,
        Action<DbSet<TEntity>, IEnumerable<TEntity>> action
    )
        where TEntity : class
    {
        var entitySet =
            context
                .Set<TEntity>();

        action(
            entitySet,
            entities
        );
    }

    private Task<int> SaveChangesAsync(
        CancellationToken cancellationToken
    ) =>
        context
            .SaveChangesAsync(
                cancellationToken
            );
}