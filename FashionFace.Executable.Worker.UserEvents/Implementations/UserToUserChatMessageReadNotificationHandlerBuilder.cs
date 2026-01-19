using System.Text;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Extensions.Implementations;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.Serialization.Interfaces;
using FashionFace.Dependencies.SignalR.Interfaces;
using FashionFace.Dependencies.SignalR.Models;
using FashionFace.Executable.Worker.UserEvents.Args;
using FashionFace.Executable.Worker.UserEvents.Interfaces;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Strategy.Builders.Args;
using FashionFace.Repositories.Strategy.Builders.Interfaces;
using FashionFace.Repositories.Strategy.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client.Events;

namespace FashionFace.Executable.Worker.UserEvents.Implementations;

public sealed class UserToUserChatMessageReadNotificationHandlerBuilder :
    IUserToUserChatMessageReadNotificationHandlerBuilder
{
    private const int BatchCount = 5;

    public AsyncEventHandler<BasicDeliverEventArgs> Build(
        EventHandlerBuilderArgs args
    ) =>
        async (
            _,
            eventArgs
        ) =>
        {
            using var scope =
                args
                    .ServiceProvider
                    .CreateScope();

            var serviceProvider =
                scope.ServiceProvider;

            var logger =
                serviceProvider.GetRequiredService<ILogger<UserToUserChatMessageReadNotificationHandlerBuilder>>();

            var serializationDecorator =
                serviceProvider.GetRequiredService<ISerializationDecorator>();

            var exceptionDescriptor =
                serviceProvider.GetRequiredService<IExceptionDescriptor>();

            var guidGenerator =
                serviceProvider.GetRequiredService<IGuidGenerator>();

            var userToUserChatNotificationsHubService =
                serviceProvider.GetRequiredService<IUserToUserChatNotificationsHubService>();

            var outboxBatchStrategy =
                serviceProvider.GetRequiredService<IOutboxBatchStrategy>();

            var correlatedSelectPendingStrategyBuilder =
                serviceProvider.GetRequiredService<ICorrelatedSelectPendingStrategyBuilder>();

            var state =
                new
                {
                    HandleId = guidGenerator.GetNew(),
                };

            using var loggerScope =
                logger
                    .BeginScope(
                        state
                    );

            var messageAsString =
                GetMessageAsString(
                    eventArgs
                );

            var eventMessage =
                serializationDecorator
                    .Deserialize<HandleUserToUserMessageReadNotificationOutbox>(
                        messageAsString
                    );

            if (eventMessage is null)
            {
                throw exceptionDescriptor.Exception(
                    "InvalidMessageType"
                );
            }

            var selectPendingStrategyBuilderArgs =
                new CorrelatedSelectPendingStrategyBuilderArgs(
                    eventMessage.CorrelationId,
                    BatchCount
                );

            var outboxBatchStrategyArgs =
                correlatedSelectPendingStrategyBuilder
                    .Build<UserToUserChatMessageReadNotificationOutbox>(
                        selectPendingStrategyBuilderArgs
                    );

            var outboxList =
                await
                    outboxBatchStrategy
                        .ClaimBatchAsync<UserToUserChatMessageReadNotificationOutbox>(
                            outboxBatchStrategyArgs
                        );

            while (outboxList.IsNotEmpty())
            {
                foreach (var outbox in outboxList)
                {
                    var message =
                        new MessageReadMessage(
                            outbox.ChatId,
                            outbox.InitiatorUserId,
                            outbox.MessageId
                        );

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

                outboxList =
                    await
                        outboxBatchStrategy
                            .ClaimBatchAsync<UserToUserChatMessageReadNotificationOutbox>(
                                outboxBatchStrategyArgs
                            );
            }
        };

    private static string GetMessageAsString(
        BasicDeliverEventArgs basicDeliverEventArgs
    )
    {
        var body =
            basicDeliverEventArgs
                .Body
                .Span;

        var message =
            Encoding
                .UTF8
                .GetString(
                    body
                );

        return
            message;
    }
}