using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Interfaces;
using FashionFace.Dependencies.RabbitMq.Model;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Implementations;

public sealed class PublishSubscribeChannelService(
    IQueueChannelService queueChannelService
) : IPublishSubscribeChannelService
{
    public async Task<PublishSubscribeChannel> CreateFanout(
        IConnection connection,
        string exchange,
        string queue,
        bool durable = true
    ) =>
        await
            Create(
                connection,
                exchange,
                queue,
                ExchangeType.Fanout,
                durable
            );

    public async Task<PublishSubscribeChannel> CreateDirect(
        IConnection connection,
        string exchange,
        string queue,
        bool durable = true
    ) =>
        await
            Create(
                connection,
                exchange,
                queue,
                ExchangeType.Direct,
                durable
            );

    public async Task<PublishSubscribeChannel> Create(
        IConnection connection,
        string exchange,
        string queue,
        string exchangeType,
        bool durable = true
    )
    {
        var channel =
            await
                queueChannelService
                    .CreateInstance(
                        connection
                    );

        await
            channel
                .ExchangeDeclareAsync(
                    exchange,
                    exchangeType
                );

        await
            channel
                .QueueDeclareAsync(
                    queue,
                    false,
                    false,
                    false
                );

        var publishSubscribeChannel =
            new PublishSubscribeChannel(
                channel,
                exchange,
                queue
            );

        return
            publishSubscribeChannel;
    }
}