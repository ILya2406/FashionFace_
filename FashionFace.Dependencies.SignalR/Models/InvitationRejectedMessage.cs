using System;

namespace FashionFace.Dependencies.SignalR.Models;

public sealed record InvitationRejectedMessage(
    Guid InvitationId,
    Guid InitiatorUserId
);
