using System.Collections.Generic;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Extensions.Models;

namespace FashionFace.Services.Singleton;

public sealed class DependencyManager :
    IDependencyManager
{
    public IReadOnlyList<DependencyBase> GetDependencies() =>
        typeof(DependencyManager).GetSingletonDependencies();
}