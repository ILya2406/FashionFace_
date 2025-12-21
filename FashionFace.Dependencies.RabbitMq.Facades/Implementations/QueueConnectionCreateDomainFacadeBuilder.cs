using FashionFace.Services.ConfigurationSettings.Implementations;
using FashionFace.Services.ConfigurationSettings.Models;

using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;
using FashionFace.Dependencies.RabbitMq.Implementations;

namespace FashionFace.Dependencies.RabbitMq.Facades.Implementations;

public sealed class QueueConnectionCreateDomainFacadeBuilder : IQueueConnectionCreateDomainFacadeBuilder
{
    public IQueueConnectionCreateDomainFacade Build()
    {
        const string RabbitMq =
            "RabbitMq";

        var rabbitMqSettings =
            new RabbitMqSettings();

        var optionsBuilder =
            new OptionsBuilder();

        var options =
            optionsBuilder
                .Build(
                    rabbitMqSettings,
                    RabbitMq
                );

        var rabbitMqSettingsFactory =
            new RabbitMqSettingsFactory(
                options
            );

        var queueConnectionService =
            new QueueConnectionService();

        var queueConnectionCreateDomainFacade =
            new QueueConnectionCreateDomainFacade(
                rabbitMqSettingsFactory,
                queueConnectionService
            );

        return
            queueConnectionCreateDomainFacade;
    }
}