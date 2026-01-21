using System.Linq;
using System;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Facades.Users.Args.UserToUserChats;
using FashionFace.Facades.Users.Interfaces.UserToUserChats;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;

using Microsoft.EntityFrameworkCore;

using Npgsql;

namespace FashionFace.Facades.Users.Implementations.UserToUserChats;

public sealed class UserToUserChatMessageReadFacade(
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository,
    ICreateRepository createRepository,
    ITransactionManager transactionManager,
    ICommandSendService commandSendService
): IUserToUserChatMessageReadFacade
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
                        entity => entity.MessageId == messageId
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

        userToUserChatProfile.LastReadAt =
            message.CreatedAt;

        // Проверяем, нет ли уже записи в Outbox для этого сообщения
        var outboxCollection =
            genericReadRepository.GetCollection<UserToUserChatMessageReadOutbox>();

        var existingOutbox =
            await
                outboxCollection
                    .FirstOrDefaultAsync(
                        entity => entity.MessageId == message.Id
                    );

        using var transaction =
            await
                transactionManager.BeginTransaction();

        await
            updateRepository
                .UpdateAsync(
                    userToUserChatProfile
                );

        // Создаем запись в Outbox только если ее еще нет
        if (existingOutbox is null)
        {
            var userToUserChatMessageOutbox =
                new UserToUserChatMessageReadOutbox
                {
                    Id = Guid.NewGuid(),
                    ChatId = chatId,
                    MessageId = message.Id,
                    InitiatorUserId = userId,
                    AttemptCount = 0,
                    OutboxStatus = OutboxStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    CorrelationId = Guid.NewGuid(),
                    ClaimedAt = null,
                };

            try
            {
                await
                    createRepository
                        .CreateAsync(
                            userToUserChatMessageOutbox
                        );

                await
                    transaction.CommitAsync();

                // Publish event to RabbitMQ for immediate processing
                var command =
                    new HandleUserToUserMessageReadOutbox(
                        userToUserChatMessageOutbox.CorrelationId
                    );

                await
                    commandSendService
                        .SendAsync(
                            command
                        );
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23505" })
            {
                // Duplicate key - outbox already exists (race condition), ignore
            }
        }
        else
        {
            await
                transaction.CommitAsync();
        }
    }
}