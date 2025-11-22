using Microsoft.EntityFrameworkCore;

namespace FashionFace.Repositories.Read.Interfaces;

public interface IGenericReadRepository
{
    DbSet<T> GetCollection<T>()
        where T : class;
}