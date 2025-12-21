using System.Threading;
using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Interfaces;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Implementations;

public sealed class QueueConnectionService :
    IQueueConnectionService
{
    private readonly SemaphoreSlim semaphore =
        new(
            1,
            1
        );

    private Task<IConnection>? connection;

    public async Task<IConnection> CreateInstance(
        string hostName,
        string vHostName,
        string username,
        string password,
        int port
    )
    {
        if (connection is not null)
        {
            return
                await
                    connection;
        }

        await
            semaphore.WaitAsync();

        try
        {
            if (connection is null)
            {
                var factory =
                    new ConnectionFactory
                    {
                        HostName = hostName,
                        VirtualHost = vHostName,
                        Port = port,
                        UserName = username,
                        Password = password,
                    };

                connection =
                    factory.CreateConnectionAsync();
            }
        }
        finally
        {
            semaphore.Release();
        }

        return
            await
                connection;
    }
}