using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Extensions.Models;

namespace FashionFace.Dependencies.Logger;

public sealed class DependencyManager :
    IDependencyManager
{
    public IReadOnlyList<DependencyBase> GetDependencies() =>
        typeof(DependencyManager).GetSingletonDependencies();
}