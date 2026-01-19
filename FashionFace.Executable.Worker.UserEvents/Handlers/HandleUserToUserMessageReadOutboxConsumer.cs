using System.Linq;
using System.Threading.Tasks;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using MassTransit;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Handlers;

public sealed class HandleUserToUserMessageReadOutboxConsumer(
    IGuidGenerator guidGenerator,
    IDateTimePicker dateTimePicker,
    IBulkUpdateRepository bulkUpdateRepository,
    IGenericReadRepository genericReadRepository,
    IExceptionDescriptor exceptionDescriptor,
    IUpdateRepository updateRepository,
    ITransactionManager transactionManager,
    ICommandSendService commandSendService,
    ILogger<HandleUserToUserMessageReadOutboxConsumer> logger
) : IConsumer<HandleUserToUserMessageReadOutbox>
{
    public async Task Consume(
        ConsumeContext<HandleUserToUserMessageReadOutbox> context
    )
    {
        var eventMessage =
            context.Message;

        using var loggerScope =
            logger
                .BeginScope(
                    new
                    {
                        eventMessage.CorrelationId,
                    }
                );

        var outboxConsumerName =
            nameof(HandleUserToUserMessageReadOutboxConsumer);

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} started for {eventMessage.CorrelationId}"
            );

        var updatedCount =
            await
                bulkUpdateRepository
                    .ExecuteUpdateAsync<UserToUserChatMessageReadOutbox>(
                        entity =>
                            entity.CorrelationId == eventMessage.CorrelationId
                            && entity.OutboxStatus == OutboxStatus.Pending,
                        entity =>
                            entity.SetProperty(
                                outbox => outbox.OutboxStatus,
                                OutboxStatus.Claimed
                            )
                    );

        if (updatedCount == 0)
        {
            logger
                .LogInformation(
                    "Nothing to handle"
                );

            return;
        }

        var userToUserChatMessageReadOutboxCollection =
            genericReadRepository.GetCollection<UserToUserChatMessageReadOutbox>();

        var outbox =
            await
                userToUserChatMessageReadOutboxCollection
                    .FirstOrDefaultAsync(
                        entity => entity.CorrelationId == eventMessage.CorrelationId
                    );

        if (outbox is null)
        {
            throw exceptionDescriptor.Exception(
                "NothingToHandle"
            );
        }

        var chatId = outbox.ChatId;
        var messageId = outbox.MessageId;
        var initiatorUserId = outbox.InitiatorUserId;

        var userToUserChatCollection =
            genericReadRepository.GetCollection<UserToUserChat>();

        var userToUserChatUserIdList =
            await
                userToUserChatCollection
                    .Where(
                        entity => entity.Id == chatId
                    )
                    .Select(
                        entity =>
                            entity
                                .UserCollection
                                .Select(
                                    user => user.ApplicationUserId
                                )
                                .ToList()
                    )
                    .FirstOrDefaultAsync();

        var initiatorBelongToUserTiUserChat =
            userToUserChatUserIdList?
                .Any(
                    id => id == initiatorUserId
                )
            ?? false;

        if (!initiatorBelongToUserTiUserChat)
        {
            outbox.OutboxStatus = OutboxStatus.Failed;

            await
                updateRepository
                    .UpdateAsync(
                        outbox
                    );

            throw exceptionDescriptor.NotFound<UserToUserChat>();
        }

        var userToUserChatMessageReadNotificationOutboxList =
            userToUserChatUserIdList!
                .Where(
                    entity => entity != initiatorUserId
                )
                .Select(
                    targetUserId =>
                        new UserToUserChatMessageReadNotificationOutbox
                        {
                            Id = guidGenerator.GetNew(),
                            ChatId = chatId,
                            MessageId = messageId,
                            InitiatorUserId = initiatorUserId,
                            TargetUserId = targetUserId,

                            CreatedAt = dateTimePicker.GetUtcNow(),
                            CorrelationId = outbox.CorrelationId,
                            AttemptCount = 0,
                            OutboxStatus = OutboxStatus.Pending,
                            ClaimedAt = null,
                        }
                )
                .ToList();

        using var transaction =
            await
                transactionManager.BeginTransaction();

        await
            updateRepository
                .UpdateCollectionAsync(
                    userToUserChatMessageReadNotificationOutboxList
                );

        outbox.OutboxStatus = OutboxStatus.Done;

        await
            updateRepository
                .UpdateAsync(
                    outbox
                );

        await
            transaction.CommitAsync();

        var command =
            new HandleUserToUserMessageReadNotificationOutbox(
                outbox.CorrelationId
            );

        await
            commandSendService
                .SendAsync(
                    command
                );

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }
}