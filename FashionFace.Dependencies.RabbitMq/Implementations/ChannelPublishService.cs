using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Interfaces;
using FashionFace.Dependencies.RabbitMq.Model;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Implementations;

public sealed class ChannelPublishService :
    IChannelPublishService
{
    public async Task Publish(
        PublishSubscribeChannel channel,
        ReadOnlyMemory<byte> body,
        string routingKey
    )
    {
        await
            channel
                .Channel
                .BasicPublishAsync(
                    channel.Exchange,
                    routingKey,
                    body
                );
    }
}