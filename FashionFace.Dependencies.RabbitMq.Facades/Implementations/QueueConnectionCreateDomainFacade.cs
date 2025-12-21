using System.Threading.Tasks;

using FashionFace.Services.ConfigurationSettings.Interfaces;

using RabbitMQ.Client;

using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;
using FashionFace.Dependencies.RabbitMq.Interfaces;

namespace FashionFace.Dependencies.RabbitMq.Facades.Implementations;

public sealed class QueueConnectionCreateDomainFacade(
    IRabbitMqSettingsFactory rabbitMqSettingsFactory,
    IQueueConnectionService queueConnectionService
) : IQueueConnectionCreateDomainFacade
{
    public async Task<IConnection> CreateAsync()
    {
        var settings =
            rabbitMqSettingsFactory.GetSettings();

        return
            await
                queueConnectionService
                    .CreateInstance(
                        settings.Host,
                        settings.VHost,
                        settings.UserName,
                        settings.Password,
                        settings.Port
                    );
    }
}