using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FashionFace.Common.Extensions.Models;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace FashionFace.Common.Extensions.Implementations;

public static class SolutionDependencies
{
    private const string ExpectedAssemblyNameStart =
        "FashionFace.";

    public static IServiceCollection SetupDependencies(
        this IServiceCollection services
    )
    {
        var assemblyArray =
            GetAssemblyArray();

        var dependencies =
            assemblyArray.GetSolutionDependencies();

        return
            services
                .RegisterDependencies(
                    dependencies
                );
    }

    private static Assembly[] GetAssemblyArray()
    {
        var dependencyAssemblies =
            DependencyContext
                .Default!
                .RuntimeLibraries
                .Where(
                    NameStartWith(
                        ExpectedAssemblyNameStart
                    )
                );

        var assemblies =
            new List<Assembly>();

        foreach (var dependencyAssembly in dependencyAssemblies)
        {
            var assemblyName =
                new AssemblyName(
                    dependencyAssembly.Name
                );

            var assembly =
                Assembly
                    .Load(
                        assemblyName
                    );

            assemblies
                .Add(
                    assembly
                );
        }

        return
            assemblies.ToArray();
    }

    private static Func<RuntimeLibrary, bool> NameStartWith(string start) =>
        assembly =>
            assembly
                .Name
                .StartsWith(
                    start
                );

    private static IReadOnlyList<DependencyBase>[]
        GetSolutionDependencies(
            this Assembly[] assemblyList
        )
    {
        var types =
            assemblyList
                .SelectMany(
                    module =>
                        module.GetTypes()
                );

        var typeList =
            types
                .Where(
                    IsNotAbstractClass
                )
                .Where(
                    IsDerivedFromIDependencyManager
                )
                .ToList();

        var instances =
            new List<IDependencyManager>();

        foreach (var type in typeList)
        {
            var instanceObject =
                Activator
                    .CreateInstance(
                        type
                    );

            var instance =
                (IDependencyManager)
                instanceObject!;

            instances
                .Add(
                    instance
                );
        }

        return
            instances
                .Select(
                    manager =>
                        manager.GetDependencies()
                )
                .ToArray();
    }

    private static bool IsNotAbstractClass(
        Type type
    ) =>
        type is { IsAbstract: false, IsClass: true, };

    private static bool IsDerivedFromIDependencyManager(
        Type type
    )
    {
        var isClass =
            type.IsClass;

        var memberInfo =
            type
                .GetInterface(
                    nameof(IDependencyManager)
                );

        var isNotEmpty =
            memberInfo != default;

        return
            isClass
            && isNotEmpty;
    }

    private static IServiceCollection RegisterDependencies(
        this IServiceCollection services,
        params IReadOnlyList<DependencyBase>[] dependenciesArray
    )
    {
        foreach (var dependencies in dependenciesArray)
        {
            foreach (var dependency in dependencies)
            {
                var (
                    @interface,
                    implementation,
                    lifeTimeType
                    ) = dependency;

                var isScoped =
                    lifeTimeType
                    == LifeTimeType.Scoped;

                var serviceLifetime =
                    isScoped
                        ? ServiceLifetime.Scoped
                        : ServiceLifetime.Singleton;

                var serviceDescriptor =
                    new ServiceDescriptor(
                        @interface,
                        implementation,
                        serviceLifetime
                    );

                services
                    .Add(
                        serviceDescriptor
                    );
            }
        }

        return
            services;
    }
}