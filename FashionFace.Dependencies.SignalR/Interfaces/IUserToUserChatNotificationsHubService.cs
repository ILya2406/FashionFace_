using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Models;

namespace FashionFace.Dependencies.SignalR.Interfaces;

public interface IUserToUserChatNotificationsHubService
{
    Task NotifyMessageRead(
        Guid userId,
        MessageReadMessage message
    );

    Task NotifyMessageReceived(
        Guid userId,
        MessageReceivedMessage message
    );
}