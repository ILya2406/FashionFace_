using System.Collections.Generic;

namespace FashionFace.Common.Extensions.Dependencies.Models;

public interface IDependencyManager
{
    IReadOnlyList<DependencyBase> GetDependencies();
}