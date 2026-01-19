using FashionFace.Dependencies.RabbitMq.Facades.Args;

namespace FashionFace.Dependencies.RabbitMq.Facades.Interfaces;

public interface IQueuePublishFacadeCommandBuilder
{
    QueuePublishFacadeArgs<TEvent> Build<TEvent>(
        TEvent eventModel
    );
}