using System.Threading.Tasks;

using FashionFace.Dependencies.RabbitMq.Facades.Args;

namespace FashionFace.Dependencies.RabbitMq.Facades.Interfaces;

public interface IQueuePublishFacade
{
    Task PublishAsync<TModel>(
        QueuePublishFacadeArgs<TModel> rgs
    );
}