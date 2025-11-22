using System.Linq;

namespace FashionFace.Common.Extensions.Implementations;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyPaging<T>(
        this IQueryable<T> query,
        int? limit,
        int? offset
    )
    {
        if (offset is > 0)
        {
            query = query.Skip(
                offset.Value
            );
        }

        if (limit is > 0)
        {
            query = query.Take(
                limit.Value
            );
        }

        return query;
    }
}