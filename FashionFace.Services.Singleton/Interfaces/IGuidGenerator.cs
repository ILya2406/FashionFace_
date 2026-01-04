using System;

namespace FashionFace.Services.Singleton.Interfaces;

public interface IGuidGenerator
{
    Guid GetNew();
}