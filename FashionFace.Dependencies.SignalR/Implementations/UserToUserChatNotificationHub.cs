using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Interfaces;

namespace FashionFace.Dependencies.SignalR.Implementations;

public sealed class UserToUserChatNotificationHub : HubBase<IUserToUserChatNotificationApi>
{
    public override async Task OnConnectedAsync()
    {
        var user =
            Context.User!;

        var role =
            GetRole(
                user
            );

        if (role is null)
        {
            Context.Abort();
        }
    }

    public override async Task OnDisconnectedAsync(
        Exception? exception
    ) { }
}