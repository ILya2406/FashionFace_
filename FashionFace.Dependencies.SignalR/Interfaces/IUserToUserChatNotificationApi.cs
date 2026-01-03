using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Models;

namespace FashionFace.Dependencies.SignalR.Interfaces;

public interface IUserToUserChatNotificationApi
{
    Task MessageReceived(
        MessageReceivedMessage message
    );

    Task MessageRead(
        MessageReadMessage message
    );
}