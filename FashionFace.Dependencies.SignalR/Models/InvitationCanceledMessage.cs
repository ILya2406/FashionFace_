using System;

namespace FashionFace.Dependencies.SignalR.Models;

public sealed record InvitationCanceledMessage(
    Guid InvitationId,
    Guid InitiatorUserId
);
