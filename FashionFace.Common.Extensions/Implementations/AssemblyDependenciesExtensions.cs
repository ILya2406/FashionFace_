using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FashionFace.Common.Extensions.Models;

using static FashionFace.Common.Extensions.Models.LifeTimeType;

namespace FashionFace.Common.Extensions.Implementations;

public static class AssemblyDependenciesExtensions
{
    public static IReadOnlyList<DependencyBase> GetSingletonDependencies(
        this Type type
    ) =>
        type
            .Assembly
            .GetDependencies(
                Singleton
            );

    public static IReadOnlyList<DependencyBase> GetScopedDependencies(
        this Type type
    ) =>
        type
            .Assembly
            .GetDependencies(
                Scoped
            );

    private static IReadOnlyList<DependencyBase> GetDependencies(
        this Assembly assembly,
        LifeTimeType lifeTime
    )
    {
        var types =
            assembly.GetTypes();

        var interfaceTypes =
            types
                .Where(
                    IsInterface
                )
                .ToList();

        var implementationTypes =
            types
                .Where(
                    IsNotAbstractClass
                )
                .ToList();

        return
            GetDependencies(
                lifeTime,
                interfaceTypes,
                implementationTypes
            );
    }

    private static IReadOnlyList<DependencyBase> GetDependencies(
        LifeTimeType lifeTime,
        IReadOnlyList<Type> interfaceTypes,
        IReadOnlyList<Type> implementationTypes
    )
    {
        var result =
            new List<DependencyBase>();

        foreach (var interfaceType in interfaceTypes)
        {
            var implementations =
                GetImplementations(
                    implementationTypes,
                    interfaceType
                );

            var shouldBeSkipped =
                ShouldBeSkipped(
                    implementations
                );

            if (shouldBeSkipped)
            {
                continue;
            }

            var implementation =
                implementations.First();

            var isScoped =
                lifeTime == Scoped;

            DependencyBase dependencyItem =
                isScoped
                    ? new ScopeDependency(
                        interfaceType,
                        implementation
                    )
                    : new SingletonDependency(
                        interfaceType,
                        implementation
                    );

            result
                .Add(
                    dependencyItem
                );
        }

        return
            result;
    }

    private static IReadOnlyList<Type> GetImplementations(
        IReadOnlyList<Type> implementationTypes,
        Type interfaceType
    ) =>
        implementationTypes
            .Where(
                interfaceType
                    .IsAssignableFrom
            )
            .ToList();

    //todo : should be better way. attribute ?
    private static bool ShouldBeSkipped(
        IReadOnlyList<Type> implementations
    )
    {
        var isEmpty =
            implementations.IsEmpty();

        var hasMoreThenOneImplementation =
            implementations.Count > 1;

        return
            isEmpty
            || hasMoreThenOneImplementation;
    }

    private static bool IsInterface(
        Type type
    ) =>
        type.IsInterface;

    private static bool IsNotAbstractClass(
        Type type
    ) =>
        type is { IsAbstract: false, IsClass: true, };
}