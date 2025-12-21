using System.Threading.Tasks;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Interfaces;

public interface IQueueConnectionService
{
    Task<IConnection> CreateInstance(
        string hostName,
        string vHostName,
        string username,
        string password,
        int port
    );
}