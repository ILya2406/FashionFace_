using System.Collections.Generic;

using FashionFace.Common.Extensions.Models;

namespace FashionFace.Common.Extensions.Implementations;

public static class DependencyCollectionExtensions
{
    public static ICollection<DependencyBase> AddScoped<T>(this ICollection<DependencyBase> dependencies)
    {
        var type = typeof(T);
        dependencies.Add(
            new ScopeDependency(
                type,
                type
            )
        );
        return dependencies;
    }

    public static ICollection<DependencyBase> AddScoped<TTarget, TImpl>(this ICollection<DependencyBase> dependencies)
        where TImpl : TTarget
    {
        dependencies.Add(
            new ScopeDependency(
                typeof(TTarget),
                typeof(TImpl)
            )
        );
        return dependencies;
    }

    public static ICollection<DependencyBase> AddSingleton<TTarget, TImpl>(
        this ICollection<DependencyBase> dependencies
    )
        where TImpl : TTarget
    {
        dependencies.Add(
            new SingletonDependency(
                typeof(TTarget),
                typeof(TImpl)
            )
        );
        return dependencies;
    }
}