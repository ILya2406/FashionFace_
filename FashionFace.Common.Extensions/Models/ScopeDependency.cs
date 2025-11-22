using System;

namespace FashionFace.Common.Extensions.Models;

public sealed record ScopeDependency(
    Type Interface,
    Type Implementation
) : DependencyBase(
    Interface,
    Implementation,
    LifeTimeType.Scoped
)
{
    public static implicit operator ScopeDependency(
        (Type iterface, Type implementation) pair
    ) =>
        new(
            pair.iterface,
            pair.implementation
        );
}