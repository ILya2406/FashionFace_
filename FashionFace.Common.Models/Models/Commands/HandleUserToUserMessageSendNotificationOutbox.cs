using System;

namespace FashionFace.Common.Models.Models.Commands;

public sealed record HandleUserToUserMessageSendNotificationOutbox(
    Guid CorrelationId
);