using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;

using Microsoft.AspNetCore.SignalR;

namespace FashionFace.Dependencies.SignalR.Implementations;

public sealed class UserToUserChatInvitationNotificationsHubService(
    IHubContext<UserToUserChatInvitationNotificationHub, IUserToUserChatInvitationNotificationApi> userToUserChatInvitationNotificationHubContext
) : IUserToUserChatInvitationNotificationsHubService
{
    public async Task NotifyInvitationReceived(
        Guid userId,
        InvitationReceivedMessage message
    ) =>
        await
            userToUserChatInvitationNotificationHubContext
                .Clients
                .User(
                    userId.ToString()
                )
                .InvitationReceived(
                    message
                );

    public async Task NotifyInvitationCanceled(
        Guid userId,
        InvitationCanceledMessage message
    ) =>
        await
            userToUserChatInvitationNotificationHubContext
                .Clients
                .User(
                    userId.ToString()
                )
                .InvitationCanceled(
                    message
                );

    public async Task NotifyInvitationAccepted(
        Guid userId,
        InvitationAcceptedMessage message
    ) =>
        await
            userToUserChatInvitationNotificationHubContext
                .Clients
                .User(
                    userId.ToString()
                )
                .InvitationAccepted(
                    message
                );

    public async Task NotifyInvitationRejected(
        Guid userId,
        InvitationRejectedMessage message
    ) =>
        await
            userToUserChatInvitationNotificationHubContext
                .Clients
                .User(
                    userId.ToString()
                )
                .InvitationRejected(
                    message
                );
}
