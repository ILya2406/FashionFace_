using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Implementations.Base;
using FashionFace.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Implementations;

public sealed class DeleteRepository(
    ApplicationDatabaseContext context
) : Repository(
        context
    ),
    IDeleteRepository
{
    public async Task DeleteAsync<TEntity>(
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
                .Remove(
                    entity
                );

        await
            InvokeActionAndSaveChangesAsync(
                item,
                Action,
                cancellationToken
            );
    }

    public async Task DeleteCollectionAsync<TEntity>(
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
                .RemoveRange(
                    entity
                );

        await
            InvokeActionAndSaveChangesAsync(
                items,
                (Action<DbSet<TEntity>, IEnumerable<TEntity>>)Action,
                cancellationToken
            );
    }
}