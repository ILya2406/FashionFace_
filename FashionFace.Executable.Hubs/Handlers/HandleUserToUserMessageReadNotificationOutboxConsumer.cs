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

public sealed class HandleUserToUserMessageReadNotificationOutboxConsumer(
    IUserToUserChatNotificationsHubService userToUserChatNotificationsHubService,
    IOutboxBatchStrategy outboxBatchStrategy,
    ICorrelatedSelectPendingStrategyBuilder correlatedSelectPendingStrategyBuilder,
    ILogger<HandleUserToUserMessageReadNotificationOutboxConsumer> logger
) : IConsumer<HandleUserToUserMessageReadNotificationOutbox>
{
    private const int BatchSize = 5;

    public async Task Consume(
        ConsumeContext<HandleUserToUserMessageReadNotificationOutbox> context
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
            nameof(HandleUserToUserMessageReadNotificationOutboxConsumer);

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
                .Build<UserToUserChatMessageReadNotificationOutbox>(
                    strategyArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatMessageReadNotificationOutbox>(
                        batchArgs
                    );

        while (outboxList.IsNotEmpty())
        {
            foreach (var outbox in outboxList)
            {
                var notification =
                    new MessageReadMessage(
                        outbox.ChatId,
                        outbox.InitiatorUserId,
                        outbox.MessageId
                    );

                await
                    userToUserChatNotificationsHubService
                        .NotifyMessageRead(
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
                        .ClaimBatchAsync<UserToUserChatMessageReadNotificationOutbox>(
                            batchArgs
                        );
        }

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }
}