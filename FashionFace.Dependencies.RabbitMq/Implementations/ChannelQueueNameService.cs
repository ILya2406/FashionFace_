using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Interfaces;
using FashionFace.Dependencies.RabbitMq.Model;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Implementations;

public sealed class ChannelQueueNameService :
    IChannelQueueNameService
{
    public async Task<string> GetQueueName(
        PublishSubscribeChannel channel
    )
    {
        var queueDeclare =
            await
                channel
                    .Channel
                    .QueueDeclareAsync();

        return
            queueDeclare.QueueName;
    }
}