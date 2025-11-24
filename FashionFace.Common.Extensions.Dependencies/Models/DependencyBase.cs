using System;

namespace FashionFace.Common.Extensions.Dependencies.Models;

public abstract record DependencyBase(
    Type Interface,
    Type Implementation,
    LifeTimeType LifeTimeType
);