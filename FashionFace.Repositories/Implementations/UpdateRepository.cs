using System.Collections.Generic;
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
}