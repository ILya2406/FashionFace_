using FashionFace.Repositories.Context;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Read.Implementations;

public sealed class GenericReadRepository(
    ApplicationDatabaseContext context
) : IGenericReadRepository
{
    public DbSet<T> GetCollection<T>()
        where T : class =>
        context.Set<T>();
}