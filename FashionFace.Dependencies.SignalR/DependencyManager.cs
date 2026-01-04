using System.Collections.Generic;

using FashionFace.Common.Extensions.Dependencies.Implementations;
using FashionFace.Common.Extensions.Dependencies.Models;

namespace FashionFace.Dependencies.SignalR;

public sealed class DependencyManager :
    IDependencyManager
{
    public IReadOnlyList<DependencyBase> GetDependencies() =>
        typeof(Serialization.DependencyManager).GetSingletonDependencies();
    /*[
        new ScopeDependency(
            typeof(IUserNotificationsHubService),
            typeof(UserNotificationsHubService)
        ),
        new ScopeDependency(
            typeof(IAdminNotificationsHubService),
            typeof(AdminNotificationsHubService)
        ),
        new ScopeDependency(
            typeof(IUserToUserChatNotificationsHubService),
            typeof(UserToUserChatNotificationsHubService)
        ),
    ];*/
}