using System;
using System.Collections.Generic;
using System.Text;

using FashionFace.Common.Exceptions.Interfaces;
using FashionFace.Common.Models.Models;
using FashionFace.Dependencies.Serialization.Interfaces;
using FashionFace.Executable.Worker.UserEvents.Args;
using FashionFace.Executable.Worker.UserEvents.Interfaces;
using FashionFace.Facades.Domains.Synchronization.Args;
using FashionFace.Facades.Domains.Synchronization.Interfaces;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client.Events;

namespace FashionFace.Executable.Worker.UserEvents.Implementations;

public sealed class UserProfileUpdatedEventHandlerBuilder : IUserProfileUpdatedEventHandlerBuilder
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
                serviceProvider.GetRequiredService<ILogger<UserProfileUpdatedEventHandlerBuilder>>();

            var serializationDecorator =
                serviceProvider.GetRequiredService<ISerializationDecorator>();

            var exceptionDescriptor =
                serviceProvider.GetRequiredService<IExceptionDescriptor>();

            var talentDimensionSynchronizer =
                serviceProvider.GetRequiredService<IAppearanceTraitsDimensionsSynchronizationFacade>();

            var dictionary =
                new Dictionary<string, object>
                {
                    { "HandleId", Guid.NewGuid() },
                };

            using var loggerScope =
                logger
                    .BeginScope(
                        dictionary
                    );

            var messageAsString =
                GetMessageAsString(
                    eventArgs
                );

            var eventModel =
                serializationDecorator
                    .Deserialize<AppearanceTraitsUpdatedEventModel>(
                        messageAsString
                    );

            if (eventModel is null)
            {
                throw exceptionDescriptor.Exception("InvalidMessageType");
            }

            var profileId =
                eventModel.ProfileId;

            var talentDimensionSynchronizerArgs =
                new AppearanceTraitsDimensionsSynchronizationArgs(
                    profileId
                );

            await
                talentDimensionSynchronizer
                    .SynchronizeAsync(
                        talentDimensionSynchronizerArgs
                    );
        };

    private static string GetMessageAsString(
        BasicDeliverEventArgs basicDeliverEventArgs
    )
    {
        var body =
            basicDeliverEventArgs
                .Body
                .ToArray();

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