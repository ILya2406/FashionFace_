using System;

namespace FashionFace.Dependencies.SignalR.Models;

public sealed record InvitationAcceptedMessage(
    Guid InvitationId,
    Guid InitiatorUserId,
    Guid ChatId
);
