using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.UserToUserChats;

public sealed class UserToUserChatMessageReadFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository,
    IUserToUserChatNotificationsHubService  userToUserChatNotificationsHubService
) : IUserToUserChatMessageReadFacade
{
    public async Task Execute(
        UserToUserChatMessageReadArgs args
    )
    {
        var (userId, messageId) = args;

        var userToUserChatMessageCollection =
            genericReadRepository.GetCollection<UserToUserChatMessage>();

        var message =
            await
                userToUserChatMessageCollection
                    .FirstOrDefaultAsync(
                        entity => entity.Id == messageId
                    );

        if (message is null)
        {
            throw exceptionDescriptor.NotFound<UserToUserChatMessage>();
        }

        var chatId =
            message.ChatId;

        var userToUserChatCollection =
            genericReadRepository.GetCollection<UserToUserChat>();

        var userToUserChat =
            await
                userToUserChatCollection

                    .Include(
                        entity => entity.UserCollection
                    )

                    .FirstOrDefaultAsync(
                        entity =>
                            entity.Id == chatId
                            && entity
                                .UserCollection
                                .Any(
                                    profile =>
                                        profile.ApplicationUserId == userId
                                )
                    );

        if (userToUserChat is null)
        {
            throw exceptionDescriptor.NotFound<UserToUserChat>();
        }

        var userToUserChatProfileCollection =
            genericReadRepository.GetCollection<UserToUserChatApplicationUser>();

        var userToUserChatProfile =
            await
                userToUserChatProfileCollection
                    .FirstOrDefaultAsync(
                        entity =>
                            entity.ChatId == chatId
                            && entity.ApplicationUserId == userId
                    );

        if (userToUserChatProfile is null)
        {
            throw exceptionDescriptor.NotFound<UserToUserChatApplicationUser>();
        }

        userToUserChatProfile.LastReadMessagePositionIndex =
            message.PositionIndex;

        await
            updateRepository
                .UpdateAsync(
                    userToUserChatProfile
                );

        var targetUserIdList =
            userToUserChat
                .UserCollection
                .Where(
                    entity =>
                        entity.ApplicationUserId != userId
                )
                .Select(
                    entity => entity.ApplicationUserId
                )
                .ToList();

        var messageReadMessage =
            new MessageReadMessage(
                chatId,
                userId,
                messageId
            );

        foreach (var targetUserId in targetUserIdList)
        {
            await
                userToUserChatNotificationsHubService
                    .NotifyMessageRead(
                        targetUserId,
                        messageReadMessage
                    );
        }
    }
}