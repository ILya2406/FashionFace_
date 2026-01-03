using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;

using Microsoft.AspNetCore.SignalR;

namespace FashionFace.Dependencies.SignalR.Implementations;

public sealed class UserToUserChatNotificationsHubService(
    IHubContext<UserToUserChatNotificationHub, IUserToUserChatNotificationApi> userToUserChatNotificationHubContext
) : IUserToUserChatNotificationsHubService
{
    public async Task NotifyMessageRead(
        Guid userId,
        MessageReadMessage message
    ) =>
        await
            userToUserChatNotificationHubContext
                .Clients
                .User(
                    userId.ToString()
                )
                .MessageRead(
                    message
                );

    public async Task NotifyMessageReceived(
        Guid userId,
        MessageReceivedMessage message
    ) =>
        await
            userToUserChatNotificationHubContext
                .Clients
                .User(
                    userId.ToString()
                )
                .MessageReceived(
                    message
                );
}