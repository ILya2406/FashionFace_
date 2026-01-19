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

public sealed class UserToUserChatMessageReadNotificationOutboxPendingWorker(
    IServiceProvider serviceProvider,
    ILogger<UserToUserChatMessageReadNotificationOutboxPendingWorker> logger
) : BaseBackgroundWorker<UserToUserChatMessageReadNotificationOutboxPendingWorker>(
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

        var userToUserChatNotificationsHubService =
            scopedServiceProvider.GetRequiredService<IUserToUserChatNotificationsHubService>();

        var outboxBatchStrategy =
            scopedServiceProvider.GetRequiredService<IOutboxBatchStrategy>();

        var genericSelectPendingStrategyBuilder =
            scopedServiceProvider.GetRequiredService<IGenericSelectPendingStrategyBuilder>();

        var selectPendingStrategyBuilderArgs =
            new GenericSelectPendingStrategyBuilderArgs(
                BatchCount
            );

        var outboxBatchStrategyArgs =
            genericSelectPendingStrategyBuilder
                .Build<UserToUserChatMessageReadNotificationOutbox>(
                    selectPendingStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatMessageReadNotificationOutbox>(
                        outboxBatchStrategyArgs
                    );

        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        foreach (var outbox in outboxList)
        {
            var message =
                new MessageReadMessage(
                    outbox.ChatId,
                    outbox.InitiatorUserId,
                    outbox.MessageId
                );

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await
                userToUserChatNotificationsHubService
                    .NotifyMessageRead(
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