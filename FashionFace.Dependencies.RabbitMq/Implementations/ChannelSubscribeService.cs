using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Interfaces;
using FashionFace.Dependencies.RabbitMq.Model;

using RabbitMQ.Client.Events;

namespace FashionFace.Dependencies.RabbitMq.Implementations;

public sealed class ChannelSubscribeService(
    IChannelQueueNameService
        channelQueueNameService
) : IChannelSubscribeService
{
    public async Task Subscribe(
        PublishSubscribeChannel channel,
        AsyncEventHandler<BasicDeliverEventArgs> handler
    )
    {
        await
            channel
                .Channel
                .QueueBindAsync(
                    channel.Queue,
                    channel.Exchange,
                    channel.Queue
                );

        var consumer =
            new AsyncEventingBasicConsumer(
                channel.Channel
            );

        consumer.ReceivedAsync +=
            handler;

        await
            channel
                .Channel
                .BasicConsumeAsync(
                    channel.Queue,
                    true,
                    string.Empty,
                    default,
                    default,
                    default,
                    consumer
                );
    }
}