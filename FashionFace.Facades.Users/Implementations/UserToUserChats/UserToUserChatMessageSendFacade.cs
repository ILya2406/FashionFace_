using System;
using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Constants.Constants;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Facades.Users.Models.UserToUserChats;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace FashionFace.Facades.Users.Implementations.UserToUserChats;

public sealed class UserToUserChatMessageSendFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    ICreateRepository createRepository,
    ITransactionManager  transactionManager
) : IUserToUserChatMessageSendFacade
{
    public async Task<UserToUserChatMessageSendResult> Execute(
        UserToUserChatMessageSendArgs args
    )
    {
        var (userId, chatId, message) = args;

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

        var userToUserChatMessageCollection =
            genericReadRepository.GetCollection<UserToUserChatMessage>();

        var lastMessagePositionIndex =
            await
                userToUserChatMessageCollection
                    .Where(
                        entity =>
                            entity.ChatId == chatId
                    )
                    .OrderByDescending(
                        entity => entity.PositionIndex
                    )
                    .FirstOrDefaultAsync();

        var lastPositionIndex =
            lastMessagePositionIndex?.PositionIndex
            ?? PositionIndexConstants.DefaultPositionIndex;

        var positionIndex =
            lastPositionIndex
            + PositionIndexConstants.PositionIndexShift;

        var messageId =
            Guid.NewGuid();

        var userToUserMessage =
            new UserToUserMessage
            {
                Id = messageId,
                ApplicationUserId = userId,
                Value =  message,
            };

        var createdAt =
            DateTime.UtcNow;

        var userToUserChatMessage =
            new UserToUserChatMessage
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                CreatedAt =  createdAt,
                PositionIndex = positionIndex,
                MessageId = messageId,
                Message = userToUserMessage,
            };

        var userToUserChatMessageOutboxList =
            userToUserChat
                .UserCollection
                .Where(
                    entity =>
                        entity.ApplicationUserId != userId
                )
                .Select(
                    entity => entity.ApplicationUserId
                )
                .Select(
                    targetUserId =>
                        new UserToUserChatMessageOutbox
                        {
                            Id = Guid.NewGuid(),
                            ChatId = chatId,
                            MessageId = messageId,
                            MessageValue = message,
                            MessagePositionIndex = positionIndex,
                            MessageCreatedAt = createdAt,
                            InitiatorUserId = userId,
                            TargetUserId = targetUserId,
                            AttemptCount = 0,
                            Status = OutboxStatus.Pending,
                            ProcessingStartedAt = null,
                        }
                )
                .ToList();

        using var transaction =
            await
                transactionManager.BeginTransaction();

        await
            createRepository
                .CreateAsync(
                    userToUserChatMessage
                );

        await
            createRepository
                .CreateCollectionAsync(
                    userToUserChatMessageOutboxList
                );

        await
            transaction.CommitAsync();

        var result =
            new UserToUserChatMessageSendResult(
                userToUserChatMessage.MessageId
            );

        return
            result;
    }
}