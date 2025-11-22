using System;

namespace FashionFace.Common.Extensions.Models;

public sealed record SingletonDependency(
    Type Interface,
    Type Implementation
) : DependencyBase(
    Interface,
    Implementation,
    LifeTimeType.Singleton
)
{
    public static implicit operator SingletonDependency(
        (Type iterface, Type implementation) pair
    ) =>
        new(
            pair.iterface,
            pair.implementation
        );
}