using System.Linq;

using FashionFace.Repositories.Context;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Read.Implementations;

public sealed class BypassGenericReadRepository(
    ApplicationDatabaseContext context
) : IBypassGenericReadRepository
{
    public IQueryable<T> GetCollection<T>()
        where T : class =>
        context
            .Set<T>()
            .IgnoreQueryFilters();
}