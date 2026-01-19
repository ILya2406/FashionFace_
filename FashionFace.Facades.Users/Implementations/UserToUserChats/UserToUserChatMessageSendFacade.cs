using System;
using FashionFace.Services.Singleton.Interfaces;
﻿using System.Linq;
using System.Threading.Tasks;
using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Facades.Users.Models.UserToUserChats;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.OutboxEntity;
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
    ITransactionManager  transactionManager,
    IDateTimePicker dateTimePicker
): IUserToUserChatMessageSendFacade
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

        var messageId =
            Guid.NewGuid();

        var chatMessageId =
            Guid.NewGuid();

        var createdAt =
            dateTimePicker.GetUtcNow();

        var userToUserMessage =
            new UserToUserMessage
            {
                Id = messageId,
                ApplicationUserId = userId,
                Value =  message,
                CreatedAt =  createdAt,
            };

        var userToUserChatMessage =
            new UserToUserChatMessage
            {
                Id = chatMessageId,
                ChatId = chatId,
                MessageId = messageId,
                Message = userToUserMessage,
                CreatedAt = createdAt,
            };

        var userToUserChatMessageOutbox =
            new UserToUserChatMessageSendOutbox
            {
                Id = Guid.NewGuid(),
                ChatId = chatId,
                MessageId = chatMessageId, // FK к UserToUserChatMessage, не к UserToUserMessage
                InitiatorUserId = userId,
                AttemptCount = 0,
                OutboxStatus = OutboxStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                CorrelationId = Guid.NewGuid(),
                ClaimedAt = null,
            };

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
                .CreateAsync(
                    userToUserChatMessageOutbox
                );

        await
            transaction.CommitAsync();

        var result =
            new UserToUserChatMessageSendResult(
                messageId
            );

        return
            result;
    }
}
