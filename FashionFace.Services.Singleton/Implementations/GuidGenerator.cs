using System;

using FashionFace.Services.Singleton.Interfaces;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class GuidGenerator : IGuidGenerator
{
    public Guid GetNew() =>
        Guid.NewGuid();
}