using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Extensions.Models;

namespace FashionFace.Common.Exceptions;

public sealed class DependencyManager :
    IDependencyManager
{
    public IReadOnlyList<DependencyBase> GetDependencies() =>
        typeof(DependencyManager).GetSingletonDependencies();
}