using System;

namespace FashionFace.Common.Models.Models.Commands;

public sealed record HandleUserToUserInvitationAcceptedNotificationOutbox(
    Guid CorrelationId
);