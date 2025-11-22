using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FashionFace.Common.Extensions.Implementations;

[SuppressMessage(
    "ReSharper",
    "PossibleMultipleEnumeration"
)]
public static class EnumerableExtensions
{
    public static bool IsEmpty<TEntity>(
        [NotNullWhen(
            false
        )]
        this IEnumerable<TEntity>? collection
    ) =>
        collection == null
        || !collection.Any();

    public static bool IsNotEmpty<TEntity>(
        [NotNullWhen(
            true
        )]
        this IEnumerable<TEntity>? enumerable
    ) =>
        enumerable != null
        && enumerable.Any();
}