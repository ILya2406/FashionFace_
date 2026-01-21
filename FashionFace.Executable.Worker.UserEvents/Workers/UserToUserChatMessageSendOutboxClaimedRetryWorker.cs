using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Interfaces;
using FashionFace.Repositories.Strategy.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Workers;

public sealed class UserToUserChatMessageSendOutboxClaimedRetryWorker(
    IServiceProvider serviceProvider,
    ILogger<UserToUserChatMessageSendOutboxClaimedRetryWorker> logger
) : BaseBackgroundWorker<UserToUserChatMessageSendOutboxClaimedRetryWorker>(
    logger
)
{
    private const int CycleDelayInMinutes = 5;
    private const int RetryDelayMinutes = 5;
    private const int BatchCount = 5;

    protected override async Task DoWorkAsync(
        CancellationToken cancellationToken
    )
    {
        using var scope =
            serviceProvider.CreateScope();

        var scopedServiceProvider =
            scope.ServiceProvider;

        var userToUserChatInvitationNotificationsHubService =
            scopedServiceProvider.GetRequiredService<IUserToUserChatInvitationNotificationsHubService>();

        var outboxBatchStrategy =
            scopedServiceProvider.GetRequiredService<IOutboxBatchStrategy>();

        var genericReadRepository =
            scopedServiceProvider.GetRequiredService<IGenericReadRepository>();

        var updateRepository =
            scopedServiceProvider.GetRequiredService<IUpdateRepository>();

        var createRepository =
            scopedServiceProvider.GetRequiredService<ICreateRepository>();

        var transactionManager =
            scopedServiceProvider.GetRequiredService<ITransactionManager>();

        var guidGenerator =
            scopedServiceProvider.GetRequiredService<IGuidGenerator>();

        var genericSelectClaimedRetryStrategyBuilder =
            scopedServiceProvider.GetRequiredService<IGenericSelectClaimedRetryStrategyBuilder>();

        var dateTimePicker =
            serviceProvider.GetRequiredService<IDateTimePicker>();

        var selectClaimedRetryStrategyBuilderArgs =
            new GenericSelectClaimedRetryStrategyBuilderArgs(
                BatchCount,
                RetryDelayMinutes
            );

        var outboxBatchStrategyArgs =
            genericSelectClaimedRetryStrategyBuilder
                .Build<UserToUserChatMessageSendOutbox>(
                    selectClaimedRetryStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatMessageSendOutbox>(
                        outboxBatchStrategyArgs
                    );

        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        foreach (var outbox in outboxList)
        {
            var chatId = outbox.ChatId;
            var messageId = outbox.MessageId;
            var initiatorUserId = outbox.InitiatorUserId;
            var correlationId = outbox.CorrelationId;

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
                                            profile.ApplicationUserId == initiatorUserId
                                    )
                        );

            if (userToUserChat is null)
            {
                await
                    outboxBatchStrategy
                        .MakeFailedAsync(
                            outbox
                        );

                logger
                    .LogError(
                        $"Outbox [{outbox.Id}] failed. User to user chat [{chatId}] was not found"
                    );

                continue;
            }

            var userToUserChatMessageCollection =
                genericReadRepository.GetCollection<UserToUserChatMessage>();

            var userToUserChatMessage =
                await
                    userToUserChatMessageCollection
                        .Include(
                            entity => entity.Message
                        )
                        .FirstOrDefaultAsync(
                            entity =>
                                entity.Id == messageId
                                && entity.ChatId == chatId
                        );

            if (userToUserChatMessage is null)
            {
                await
                    outboxBatchStrategy
                        .MakeFailedAsync(
                            outbox
                        );

                logger
                    .LogError(
                        $"Outbox [{outbox.Id}] failed. User to user chat message [{messageId}] was not found"
                    );

                continue;
            }

            var chatMessage =
                userToUserChatMessage.Message!;

            var message =
                chatMessage.Value;

            var createdAt =
                chatMessage.CreatedAt;

            var userToUserChatMessageSendNotificationOutboxList =
                userToUserChat
                    .UserCollection
                    .Where(
                        entity =>
                            entity.ApplicationUserId != initiatorUserId
                    )
                    .Select(
                        entity => entity.ApplicationUserId
                    )
                    .Select(
                        targetUserId =>
                            new UserToUserChatMessageSendNotificationOutbox
                            {
                                Id = guidGenerator.GetNew(),
                                ChatId = chatId,
                                MessageId = messageId,
                                MessageValue = message,
                                MessageCreatedAt = createdAt,
                                InitiatorUserId = initiatorUserId,
                                TargetUserId = targetUserId,

                                CreatedAt = dateTimePicker.GetUtcNow(),
                                CorrelationId = correlationId,
                                AttemptCount = 0,
                                OutboxStatus = OutboxStatus.Pending,
                                ClaimedAt = null,
                            }
                    )
                    .ToList();

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            using var transaction =
                await
                    transactionManager.BeginTransaction();

            await
                createRepository
                    .CreateCollectionAsync(
                        userToUserChatMessageSendNotificationOutboxList,
                        cancellationToken
                    );

            await
                outboxBatchStrategy
                    .MakeDoneAsync(
                        outbox
                    );

            await
                transaction.CommitAsync();
        }
    }

    protected override TimeSpan GetDelay() =>
        TimeSpan
            .FromMinutes(
                CycleDelayInMinutes
            );
}