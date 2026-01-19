using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.MassTransit.Interfaces;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Interfaces;
using FashionFace.Repositories.Strategy.Interfaces;

using MassTransit;

using Microsoft.Extensions.Logging;

namespace FashionFace.Executable.Worker.UserEvents.Handlers;

public sealed class HandleUserToUserInvitationAcceptedOutboxConsumer(
    IOutboxBatchStrategy outboxBatchStrategy,
    ICorrelatedSelectPendingStrategyBuilder correlatedSelectPendingStrategyBuilder,
    ICommandSendService commandSendService,
    ILogger<HandleUserToUserInvitationAcceptedOutboxConsumer> logger
) : IConsumer<HandleUserToUserInvitationAcceptedOutbox>
{
    private const int BatchSize = 5;

    public async Task Consume(
        ConsumeContext<HandleUserToUserInvitationAcceptedOutbox> context
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
            nameof(HandleUserToUserInvitationAcceptedOutboxConsumer);

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
                .Build<UserToUserChatInvitationAcceptedOutbox>(
                    selectPendingStrategyBuilderArgs
                );

        var outboxList =
            await
                outboxBatchStrategy
                    .ClaimBatchAsync<UserToUserChatInvitationAcceptedOutbox>(
                        outboxBatchStrategyArgs
                    );

        while (outboxList.IsNotEmpty())
        {
            foreach (var outbox in outboxList)
            {
                var command =
                    new HandleUserToUserInvitationAcceptedNotificationOutbox(
                        outbox.CorrelationId
                    );

                await
                    commandSendService
                        .SendAsync(
                            command
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
                        .ClaimBatchAsync<UserToUserChatInvitationAcceptedOutbox>(
                            outboxBatchStrategyArgs
                        );
        }

        logger
            .LogInformation(
                $"Consumer {outboxConsumerName} ended for {eventMessage.CorrelationId}"
            );
    }
}