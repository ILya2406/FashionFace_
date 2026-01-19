using System.Collections.Generic;

using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Common.Extensions.Dependencies.Models;

namespace FashionFace.Dependencies.MassTransit;

public sealed class DependencyManager :
    IDependencyManager
{
    public IReadOnlyList<DependencyBase> GetDependencies() =>
        typeof(DependencyManager).GetScopedDependencies();
}