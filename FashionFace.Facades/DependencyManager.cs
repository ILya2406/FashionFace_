using System.Collections.Generic;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Extensions.Models;

namespace FashionFace.Facades;

public sealed class DependencyManager :
    IDependencyManager
{
    public IReadOnlyList<DependencyBase> GetDependencies() =>
        typeof(DependencyManager).GetScopedDependencies();
}