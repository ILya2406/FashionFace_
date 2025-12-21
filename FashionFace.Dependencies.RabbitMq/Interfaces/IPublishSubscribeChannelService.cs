using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Model;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Interfaces;

public interface IPublishSubscribeChannelService
{
    Task<PublishSubscribeChannel> CreateFanout(
        IConnection connection,
        string exchange,
        string queue,
        bool durable = true
    );

    Task<PublishSubscribeChannel> CreateDirect(
        IConnection connection,
        string exchange,
        string queue,
        bool durable = true
    );

    Task<PublishSubscribeChannel> Create(
        IConnection connection,
        string exchange,
        string queue,
        string exchangeType,
        bool durable = true
    );
}