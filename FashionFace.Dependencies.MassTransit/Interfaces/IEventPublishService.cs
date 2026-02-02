using System.Threading.Tasks;

namespace FashionFace.Dependencies.MassTransit.Interfaces;

public interface IEventPublishService
{
    Task PublishAsync<TEvent>(
        TEvent @event
    ) where TEvent : class;
}