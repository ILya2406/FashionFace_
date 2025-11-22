using System;

namespace FashionFace.Common.Extensions.Models;

public abstract record DependencyBase(
    Type Interface,
    Type Implementation,
    LifeTimeType LifeTimeType
);