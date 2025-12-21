using System.Threading.Tasks;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Facades.Interfaces;

public interface IQueueConnectionCreateDomainFacade
{
    Task<IConnection> CreateAsync();
}