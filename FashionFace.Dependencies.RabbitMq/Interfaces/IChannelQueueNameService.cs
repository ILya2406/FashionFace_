using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Model;

namespace FashionFace.Dependencies.RabbitMq.Interfaces;

public interface IChannelQueueNameService
{
    Task<string> GetQueueName(
        PublishSubscribeChannel channel
    );
}