using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Model;

namespace FashionFace.Dependencies.RabbitMq.Interfaces;

public interface IChannelPublishService
{
    Task Publish(
        PublishSubscribeChannel channel,
        ReadOnlyMemory<byte> body,
        string routingKey
    );
}