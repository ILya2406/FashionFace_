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

public sealed class UserToUserChatInvitationCreateNotificationOutboxClaimedRetryWorker(
    IServiceProvider serviceProvider,
    ILogger<UserToUserChatInvitationCreateNotificationOutboxClaimedRetryWorker> logger
) : BaseBackgroundWorker<UserToUserChatInvitationCreateNotificationOutboxClaimedRetryWorker>(
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

        var genericSelectClaimedRetryStrategyBuilder =
            scopedServiceProvider.GetRequiredService<IGenericSelectClaimedRetryStrategyBuilder>();

        var selectClaimedRetryStrategyBuilderArgs =
            new GenericSelectClaimedRetryStrategyBuilderArgs(
                BatchCount,
                RetryDelayMinutes
            );

        var outboxBatchStrategyArgs =
            genericSelectClaimedRetryStrategyBuilder
                .Build<UserToUserChatInvitationCreatedOutbox>(
                    selectClaimedRetryStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatInvitationCreatedOutbox>(
                        outboxBatchStrategyArgs
                    );

        if (cancellationToken.IsCancellationRequested)
        {
            return;
        }

        foreach (var outbox in outboxList)
        {
            var message =
                new InvitationReceivedMessage(
                    outbox.InvitationId,
                    outbox.InitiatorUserId
                );

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            await
                userToUserChatInvitationNotificationsHubService
                    .NotifyInvitationReceived(
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