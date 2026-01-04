using System;

namespace FashionFace.Services.Singleton.Interfaces;

public interface IDateTimePicker
{
    DateTime GetUtcNow();
}