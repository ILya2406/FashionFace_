using System;
using System.Threading;
using System.Threading.Tasks;

using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Interfaces;
using FashionFace.Repositories.Strategy.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Workers;

public sealed class UserToUserChatMessageSendNotificationOutboxClaimedRetryWorker(
    IServiceProvider serviceProvider,
    ILogger<UserToUserChatMessageSendNotificationOutboxClaimedRetryWorker> logger
) : BaseBackgroundWorker<UserToUserChatMessageSendNotificationOutboxClaimedRetryWorker>(
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

        var userToUserChatNotificationsHubService =
            scopedServiceProvider.GetRequiredService<IUserToUserChatNotificationsHubService>();

        var outboxBatchStrategy =
            scopedServiceProvider.GetRequiredService<IOutboxBatchStrategy>();

        var genericSelectClaimedRetryStrategyBuilder =
            scopedServiceProvider.GetRequiredService<IGenericSelectClaimedRetryStrategyBuilder>();

        var selectClaimedRetryStrategyBuilderArgs =
            new GenericSelectClaimedRetryStrategyBuilderArgs(
                BatchCount,
                RetryDelayMinutes
            );

        var outboxBatchStrategyArgs =
            genericSelectClaimedRetryStrategyBuilder
                .Build<UserToUserChatMessageSendNotificationOutbox>(
                    selectClaimedRetryStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatMessageSendNotificationOutbox>(
                        outboxBatchStrategyArgs
                    );

        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        foreach (var outbox in outboxList)
        {
            var message =
                new MessageReceivedMessage(
                    outbox.ChatId,
                    outbox.InitiatorUserId,
                    outbox.MessageId,
                    outbox.MessageValue,
                    outbox.MessageCreatedAt
                );

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await
                userToUserChatNotificationsHubService
                    .NotifyMessageReceived(
                        outbox.TargetUserId,
                        message
                    );

            await
                outboxBatchStrategy
                    .MakeDoneAsync(
                        outbox
                    );
        }
    }

    protected override TimeSpan GetDelay() =>
        TimeSpan
            .FromMinutes(
                CycleDelayInMinutes
            );
}