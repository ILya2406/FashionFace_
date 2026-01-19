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

public sealed class HandleUserToUserMessageSendNotificationOutboxConsumer(
    IUserToUserChatNotificationsHubService userToUserChatNotificationsHubService,
    IOutboxBatchStrategy outboxBatchStrategy,
    ICorrelatedSelectPendingStrategyBuilder correlatedSelectPendingStrategyBuilder,
    ILogger<HandleUserToUserMessageSendNotificationOutboxConsumer> logger
) : IConsumer<HandleUserToUserMessageSendNotificationOutbox>
{
    private const int BatchSize = 5;

    public async Task Consume(
        ConsumeContext<HandleUserToUserMessageSendNotificationOutbox> context
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
            nameof(HandleUserToUserMessageSendNotificationOutboxConsumer);

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} started for {eventMessage.CorrelationId}"
            );

        var strategyArgs =
            new CorrelatedSelectPendingStrategyBuilderArgs(
                eventMessage.CorrelationId,
                BatchSize
            );

        var batchArgs =
            correlatedSelectPendingStrategyBuilder
                .Build<UserToUserChatMessageSendNotificationOutbox>(
                    strategyArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatMessageSendNotificationOutbox>(
                        batchArgs
                    );

        while (outboxList.IsNotEmpty())
        {
            foreach (var outbox in outboxList)
            {
                var notification =
                    new MessageReceivedMessage(
                        outbox.ChatId,
                        outbox.InitiatorUserId,
                        outbox.MessageId,
                        outbox.MessageValue,
                        outbox.MessageCreatedAt
                    );

                await
                    userToUserChatNotificationsHubService
                        .NotifyMessageReceived(
                            outbox.TargetUserId,
                            notification
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
                        .ClaimBatchAsync<UserToUserChatMessageSendNotificationOutbox>(
                            batchArgs
                        );
        }

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }
}