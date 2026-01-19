using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Interfaces;
using FashionFace.Repositories.Strategy.Interfaces;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Hubs.Handlers;

public sealed class HandleUserToUserInvitationRejectedNotificationOutboxConsumer(
    IUserToUserChatInvitationNotificationsHubService userToUserChatInvitationNotificationsHubService,
    IOutboxBatchStrategy outboxBatchStrategy,
    ICorrelatedSelectPendingStrategyBuilder correlatedSelectPendingStrategyBuilder,
    ILogger<HandleUserToUserInvitationRejectedNotificationOutboxConsumer> logger
) : IConsumer<HandleUserToUserInvitationRejectedNotificationOutbox>
{
    private const int BatchSize = 5;

    public async Task Consume(
        ConsumeContext<HandleUserToUserInvitationRejectedNotificationOutbox> context
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
            nameof(HandleUserToUserInvitationRejectedNotificationOutboxConsumer);

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} started for {eventMessage.CorrelationId}"
            );

        var selectPendingStrategyBuilderArgs =
            new CorrelatedSelectPendingStrategyBuilderArgs(
                eventMessage.CorrelationId,
                BatchSize
            );

        var outboxBatchStrategyArgs =
            correlatedSelectPendingStrategyBuilder
                .Build<UserToUserChatInvitationRejectedNotificationOutbox>(
                    selectPendingStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatInvitationRejectedNotificationOutbox>(
                        outboxBatchStrategyArgs
                    );

        while (outboxList.IsNotEmpty())
        {
            foreach (var outbox in outboxList)
            {
                var message =
                    new InvitationRejectedMessage(
                        outbox.InvitationId,
                        outbox.TargetUserId
                    );

                await
                    userToUserChatInvitationNotificationsHubService
                        .NotifyInvitationRejected(
                            outbox.TargetUserId,
                            message
                        );

                await
                    outboxBatchStrategy
                        .MakeDoneAsync(
                            outbox
                        );
            }

            outboxList =
                await
                    outboxBatchStrategy
                        .ClaimBatchAsync<UserToUserChatInvitationRejectedNotificationOutbox>(
                            outboxBatchStrategyArgs
                        );
        }

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }
}