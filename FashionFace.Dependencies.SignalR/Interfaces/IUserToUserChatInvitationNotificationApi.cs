using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Models;

namespace FashionFace.Dependencies.SignalR.Interfaces;

public interface IUserToUserChatInvitationNotificationApi
{
    Task InvitationReceived(
        InvitationReceivedMessage message
    );

    Task InvitationCanceled(
        InvitationCanceledMessage message
    );

    Task InvitationAccepted(
        InvitationAcceptedMessage message
    );

    Task InvitationRejected(
        InvitationRejectedMessage message
    );
}
