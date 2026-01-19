using System.Linq;
using System.Text;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Models.Models.Commands;
using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;
using FashionFace.Dependencies.Serialization.Interfaces;
using FashionFace.Executable.Worker.UserEvents.Args;
using FashionFace.Executable.Worker.UserEvents.Interfaces;
using FashionFace.Repositories.Context.Enums;
using FashionFace.Repositories.Context.Models.OutboxEntity;
using FashionFace.Repositories.Context.Models.UserToUserChats;
using FashionFace.Repositories.Interfaces;
using FashionFace.Repositories.Read.Interfaces;
using FashionFace.Repositories.Transactions.Interfaces;
using FashionFace.Services.Singleton.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client.Events;

namespace FashionFace.Executable.Worker.UserEvents.Implementations;

public sealed class UserToUserChatMessageReadHandlerBuilder : IUserToUserChatMessageReadHandlerBuilder
{
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
                serviceProvider.GetRequiredService<ILogger<UserToUserChatMessageReadHandlerBuilder>>();

            var serializationDecorator =
                serviceProvider.GetRequiredService<ISerializationDecorator>();

            var exceptionDescriptor =
                serviceProvider.GetRequiredService<IExceptionDescriptor>();

            var guidGenerator =
                serviceProvider.GetRequiredService<IGuidGenerator>();

            var bulkUpdateRepository =
                serviceProvider.GetRequiredService<IBulkUpdateRepository>();

            var genericReadRepository =
                serviceProvider.GetRequiredService<IGenericReadRepository>();

            var updateRepository =
                serviceProvider.GetRequiredService<IUpdateRepository>();

            var transactionManager =
                serviceProvider.GetRequiredService<ITransactionManager>();

            var queuePublishFacade =
                serviceProvider.GetRequiredService<IQueuePublishFacade>();

            var queuePublishFacadeCommandBuilder =
                serviceProvider.GetRequiredService<IQueuePublishFacadeCommandBuilder>();

            var dateTimePicker =
                serviceProvider.GetRequiredService<IDateTimePicker>();

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
                    .Deserialize<HandleUserToUserMessageReadOutbox>(
                        messageAsString
                    );

            if (eventMessage is null)
            {
                throw exceptionDescriptor.Exception(
                    "InvalidMessageType"
                );
            }

            var updatedCount =
                await
                    bulkUpdateRepository
                        .ExecuteUpdateAsync<UserToUserChatMessageReadOutbox>(
                            entity =>
                                entity.CorrelationId == eventMessage.CorrelationId
                                && entity.OutboxStatus == OutboxStatus.Pending,
                            entity =>
                                entity.SetProperty(
                                    outbox => outbox.OutboxStatus,
                                    OutboxStatus.Claimed
                                )
                        );

            if (updatedCount == 0)
            {
                logger
                    .LogInformation(
                        "Nothing to handle"
                    );

                return;
            }

            var userToUserChatMessageReadOutboxCollection =
                genericReadRepository.GetCollection<UserToUserChatMessageReadOutbox>();

            var outbox =
                await
                    userToUserChatMessageReadOutboxCollection
                        .FirstOrDefaultAsync(
                            entity => entity.CorrelationId == eventMessage.CorrelationId
                        );

            if (outbox is null)
            {
                throw exceptionDescriptor.Exception(
                    "NothingToHandle"
                );
            }

            var chatId = outbox.ChatId;
            var messageId = outbox.MessageId;
            var initiatorUserId = outbox.InitiatorUserId;

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
                outbox.OutboxStatus = OutboxStatus.Failed;

                await
                    updateRepository
                        .UpdateAsync(
                            outbox
                        );

                throw exceptionDescriptor.NotFound<UserToUserChat>();
            }

            var userToUserChatMessageReadNotificationOutboxList =
                userToUserChatUserIdList!
                    .Where(
                        entity => entity != initiatorUserId
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
                                CorrelationId = outbox.CorrelationId,
                                AttemptCount = 0,
                                OutboxStatus = OutboxStatus.Pending,
                                ClaimedAt = null,
                            }
                    )
                    .ToList();

            using var transaction =
                await
                    transactionManager.BeginTransaction();

            await
                updateRepository
                    .UpdateCollectionAsync(
                        userToUserChatMessageReadNotificationOutboxList
                    );

            outbox.OutboxStatus = OutboxStatus.Done;

            await
                updateRepository
                    .UpdateAsync(
                        outbox
                    );

            await
                transaction.CommitAsync();

            var handleOutbox =
                new HandleUserToUserMessageReadNotificationOutbox(
                    outbox.CorrelationId
                );

            var queuePublishFacadeArgs =
                queuePublishFacadeCommandBuilder
                    .Build(
                        handleOutbox
                    );

            await
                queuePublishFacade
                    .PublishAsync(
                        queuePublishFacadeArgs
                    );
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