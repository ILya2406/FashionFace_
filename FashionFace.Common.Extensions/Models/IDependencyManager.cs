using System.Collections.Generic;

namespace FashionFace.Common.Extensions.Models;

public interface IDependencyManager
{
    IReadOnlyList<DependencyBase> GetDependencies();
}