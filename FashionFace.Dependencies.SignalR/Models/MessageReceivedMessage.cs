using System;

namespace FashionFace.Dependencies.SignalR.Models;

public sealed record MessageReceivedMessage(
    Guid ChatId,
    Guid UserId,
    Guid MessageId,
    string MessageValue,
    DateTime MessageCreatedAt
);