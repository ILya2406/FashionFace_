using System;

namespace FashionFace.Dependencies.SignalR.Models;

public sealed record InvitationReceivedMessage(
    Guid InvitationId,
    Guid InitiatorUserId
);
