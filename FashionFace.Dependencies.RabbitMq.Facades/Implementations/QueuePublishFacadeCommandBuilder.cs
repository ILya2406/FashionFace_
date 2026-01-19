using FashionFace.Dependencies.RabbitMq.Facades.Args;
using FashionFace.Dependencies.RabbitMq.Facades.Interfaces;

namespace FashionFace.Dependencies.RabbitMq.Facades.Implementations;

public sealed class QueuePublishFacadeCommandBuilder :
    IQueuePublishFacadeCommandBuilder
{
    public QueuePublishFacadeArgs<TEvent> Build<TEvent>(
        TEvent eventModel
    )
    {
        var fullName =
            typeof(TEvent)
                .FullName!
                .ToLower();

        var exchange = $"{fullName}.exchange";
        var queue = $"{fullName}.queue";

        var queuePublishFacadeArgs =
            new QueuePublishFacadeArgs<TEvent>(
                eventModel,
                exchange,
                queue,
                "direct",
                queue,
                true
            );

        return
            queuePublishFacadeArgs;
    }
}