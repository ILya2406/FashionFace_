using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Models;

namespace FashionFace.Dependencies.SignalR.Interfaces;

public interface IUserToUserChatInvitationNotificationsHubService
{
    Task NotifyInvitationReceived(
        Guid userId,
        InvitationReceivedMessage message
    );

    Task NotifyInvitationCanceled(
        Guid userId,
        InvitationCanceledMessage message
    );

    Task NotifyInvitationAccepted(
        Guid userId,
        InvitationAcceptedMessage message
    );

    Task NotifyInvitationRejected(
        Guid userId,
        InvitationRejectedMessage message
    );
}
