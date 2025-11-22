using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Repositories.Interfaces.Base;

namespace FashionFace.Repositories.Interfaces;

public interface ICreateRepository : IRepository
{
    Task CreateAsync<TEntity>(
        TEntity item,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;

    Task CreateCollectionAsync<TEntity>(
        IEnumerable<TEntity> items,
        CancellationToken cancellationToken = default
    )
        where TEntity : class;
}