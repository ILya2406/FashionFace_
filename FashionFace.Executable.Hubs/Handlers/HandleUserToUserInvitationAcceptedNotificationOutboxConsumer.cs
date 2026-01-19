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

public sealed class HandleUserToUserInvitationAcceptedNotificationOutboxConsumer(
    IUserToUserChatInvitationNotificationsHubService userToUserChatInvitationNotificationsHubService,
    IOutboxBatchStrategy outboxBatchStrategy,
    ICorrelatedSelectPendingStrategyBuilder correlatedSelectPendingStrategyBuilder,
    ILogger<HandleUserToUserInvitationAcceptedNotificationOutboxConsumer> logger
) : IConsumer<HandleUserToUserInvitationAcceptedNotificationOutbox>
{
    private const int BatchSize = 5;

    public async Task Consume(
        ConsumeContext<HandleUserToUserInvitationAcceptedNotificationOutbox> context
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
            nameof(HandleUserToUserInvitationAcceptedNotificationOutboxConsumer);

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
                .Build<UserToUserChatInvitationAcceptedNotificationOutbox>(
                    selectPendingStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatInvitationAcceptedNotificationOutbox>(
                        outboxBatchStrategyArgs
                    );

        while (outboxList.IsNotEmpty())
        {
            foreach (var outbox in outboxList)
            {
                var message =
                    new InvitationAcceptedMessage(
                        outbox.InvitationId,
                        outbox.InitiatorUserId,
                        outbox.ChatId
                    );

                await
                    userToUserChatInvitationNotificationsHubService
                        .NotifyInvitationAccepted(
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
                        .ClaimBatchAsync<UserToUserChatInvitationAcceptedNotificationOutbox>(
                            outboxBatchStrategyArgs
                        );
        }

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }
}