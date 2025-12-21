using System.Threading.Tasks;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Interfaces;

public interface IQueueChannelService
{
    Task<IChannel> CreateInstance(
        IConnection connection
    );
}