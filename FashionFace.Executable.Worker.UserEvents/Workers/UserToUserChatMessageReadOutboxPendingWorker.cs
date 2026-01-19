using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

public sealed class UserToUserChatMessageReadOutboxPendingWorker(
    IServiceProvider serviceProvider,
    ILogger<UserToUserChatMessageReadOutboxPendingWorker> logger
) : BaseBackgroundWorker<UserToUserChatMessageReadOutboxPendingWorker>(
    logger
)
{
    private const int CycleDelayInMinutes = 5;
    private const int BatchCount = 5;

    protected override async Task DoWorkAsync(
        CancellationToken cancellationToken
    )
    {
        using var scope =
            serviceProvider.CreateScope();

        var scopedServiceProvider =
            scope.ServiceProvider;

        var outboxBatchStrategy =
            scopedServiceProvider.GetRequiredService<IOutboxBatchStrategy>();

        var genericReadRepository =
            scopedServiceProvider.GetRequiredService<IGenericReadRepository>();

        var updateRepository =
            scopedServiceProvider.GetRequiredService<IUpdateRepository>();

        var transactionManager =
            scopedServiceProvider.GetRequiredService<ITransactionManager>();

        var guidGenerator =
            scopedServiceProvider.GetRequiredService<IGuidGenerator>();

        var genericSelectPendingStrategyBuilder =
            scopedServiceProvider.GetRequiredService<IGenericSelectPendingStrategyBuilder>();

        var dateTimePicker =
            serviceProvider.GetRequiredService<IDateTimePicker>();

        var selectPendingStrategyBuilderArgs =
            new GenericSelectPendingStrategyBuilderArgs(
                BatchCount
            );

        var outboxBatchStrategyArgs =
            genericSelectPendingStrategyBuilder
                .Build<UserToUserChatMessageReadOutbox>(
                    selectPendingStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatMessageReadOutbox>(
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
                await
                    outboxBatchStrategy
                        .MakeFailedAsync(
                            outbox
                        );

                logger
                    .LogError(
                        $"Outbox [{
                            outbox.Id
                        }] failed. User to user chat [{
                            chatId
                        }] was not found for user id [{
                            initiatorUserId
                        }]"
                    );

                continue;
            }

            var userToUserChatMessageReadNotificationOutboxList =
                userToUserChatUserIdList!
                    .Where(
                        entity =>
                            entity != initiatorUserId
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
                updateRepository
                    .UpdateCollectionAsync(
                        userToUserChatMessageReadNotificationOutboxList,
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