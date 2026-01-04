using System;

using FashionFace.Services.Singleton.Interfaces;

namespace FashionFace.Services.Singleton.Implementations;

public sealed class DateTimePicker : IDateTimePicker
{
    public DateTime GetUtcNow() =>
        DateTime.UtcNow;
}