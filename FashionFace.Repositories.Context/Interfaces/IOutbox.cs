using System;

using FashionFace.Repositories.Context.Enums;

namespace FashionFace.Repositories.Context.Interfaces;

public interface IOutbox
{
    OutboxStatus Status { get; set; }
    int AttemptCount { get; set; }
    DateTime? ProcessingStartedAt { get; set; }
}