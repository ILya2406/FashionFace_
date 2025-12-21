using System.Threading;
using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Interfaces;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Implementations;

public sealed class QueueChannelService :
    IQueueChannelService
{
    private readonly SemaphoreSlim semaphore =
        new(
            1,
            1
        );

    private Task<IChannel>? channel;

    public async Task<IChannel> CreateInstance(
        IConnection connection
    )
    {
        if (channel is not null)
        {
            return
                await
                    channel;
        }

        await
            semaphore.WaitAsync();

        try
        {
            if (channel is null)
            {
                channel =
                    connection.CreateChannelAsync();
            }
        }
        finally
        {
            semaphore.Release();
        }

        return
            await
                channel;
    }
}