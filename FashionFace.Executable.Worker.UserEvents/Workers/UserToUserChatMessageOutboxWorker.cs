using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Models;
using FashionFace.Repositories.Strategy.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Workers;

public sealed class UserToUserChatMessageOutboxWorker(
    IExecuteRepository executeRepository,
    IUpdateRepository updateRepository,
    ITransactionManager transactionManager,
    IUserToUserChatNotificationsHubService userToUserChatNotificationsHubService,
    IOutboxBatchStrategy<UserToUserChatMessageOutbox> outboxBatchStrategy,
    ILogger<UserToUserChatMessageOutboxWorker> logger
) : BaseBackgroundWorker<UserToUserChatMessageOutboxWorker>(
    logger
) {
    private const int BatchCount = 5;

    protected override async Task DoWorkAsync()
    {
        using var transaction =
            await
                transactionManager.BeginTransaction();

        var userToUserChatMessageOutboxQuery =
            executeRepository
                .FromSqlRaw<UserToUserChatMessageOutbox>(
                    """
                    SELECT *
                    FROM "UserToUserChatMessageOutbox"
                    WHERE "Status" = @Status
                    ORDER BY "MessageCreatedAt"
                    FOR UPDATE SKIP LOCKED
                    LIMIT @BatchCount
                    """,
                    [
                        new SqlParameter(
                            "Status",
                            nameof(OutboxStatus.Pending)
                        ),
                        new SqlParameter(
                            "BatchCount",
                            BatchCount
                        ),
                    ]
                );

        var userToUserChatMessageOutboxList =
            await
                userToUserChatMessageOutboxQuery.ToListAsync();

        foreach (var userToUserChatMessageOutbox in userToUserChatMessageOutboxList)
        {
            userToUserChatMessageOutbox.AttemptCount++;
            userToUserChatMessageOutbox.Status = OutboxStatus.Claimed;
            userToUserChatMessageOutbox.ProcessingStartedAt = DateTime.UtcNow;
        }

        await
            updateRepository
                .UpdateCollectionAsync(
                    userToUserChatMessageOutboxList
                );

        await
            transaction.CommitAsync();

        foreach (var userToUserChatMessageOutbox in userToUserChatMessageOutboxList)
        {
            var messageReceivedMessage =
                new MessageReceivedMessage(
                    userToUserChatMessageOutbox.ChatId,
                    userToUserChatMessageOutbox.InitiatorUserId,
                    userToUserChatMessageOutbox.MessageId,
                    userToUserChatMessageOutbox.MessageValue,
                    userToUserChatMessageOutbox.MessagePositionIndex,
                    userToUserChatMessageOutbox.MessageCreatedAt
                );

            await
                userToUserChatNotificationsHubService
                    .NotifyMessageReceived(
                        userToUserChatMessageOutbox.TargetUserId,
                        messageReceivedMessage
                    );

            userToUserChatMessageOutbox.Status = OutboxStatus.Done;

            await
                updateRepository
                    .UpdateAsync(
                        userToUserChatMessageOutbox
                    );
        }
    }
}