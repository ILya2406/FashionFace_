using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Model;

using RabbitMQ.Client.Events;

namespace FashionFace.Dependencies.RabbitMq.Interfaces;

public interface IChannelSubscribeService
{
    Task Subscribe(
        PublishSubscribeChannel channel,
        AsyncEventHandler<BasicDeliverEventArgs> handler
    );
}