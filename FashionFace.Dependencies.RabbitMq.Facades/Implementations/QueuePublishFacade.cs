using System.Threading.Tasks;

using FashionFace.Common.Extensions.Implementations;
using FashionFace.Dependencies.Serialization.Interfaces;
using FashionFace.Services.ConfigurationSettings.Interfaces;

using FashionFace.Dependencies.RabbitMq.Facades.Args;
using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;
using FashionFace.Dependencies.RabbitMq.Interfaces;

namespace FashionFace.Dependencies.RabbitMq.Facades.Implementations;

public sealed class QueuePublishFacade(
    IQueueConnectionCreateDomainFacade queueConnectionCreateDomainFacade,
    IPublishSubscribeChannelService publishSubscribeChannelService,
    IChannelPublishService channelPublishService,
    ISerializationDecorator serializationDecorator,
    IRabbitMqSettingsFactory rabbitMqSettingsFactory
) : IQueuePublishFacade
{
    public async Task PublishAsync<TModel>(
        QueuePublishFacadeArgs<TModel> args
    )
    {
        var (
            model,
            exchange,
            queue,
            exchangeType,
            routingKey,
            durable
            ) = args;

        var rabbitMqSettings =
            rabbitMqSettingsFactory.GetSettings();

        var isEnabled =
            rabbitMqSettings.IsEnabled;

        if (!isEnabled)
        {
            return;
        }

        var connection =
            await
                queueConnectionCreateDomainFacade
                    .CreateAsync();

        var chanel =
            await
                publishSubscribeChannelService
                    .Create(
                        connection,
                        exchange,
                        queue,
                        exchangeType,
                        durable
                    );

        var queueMessage =
            serializationDecorator
                .Serialize(
                    model
                );

        var bytes =
            queueMessage.GetUtf8Bytes();

        await
            channelPublishService
                .Publish(
                    chanel,
                    bytes,
                    routingKey
                );
    }
}