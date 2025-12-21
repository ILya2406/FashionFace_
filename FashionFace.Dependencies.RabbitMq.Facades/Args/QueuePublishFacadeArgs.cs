using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Facades.Args;

public sealed record QueuePublishFacadeArgs<TModel>(
    TModel Model,
    string Exchange,
    string Queue,
    string ExchangeType = ExchangeType.Fanout,
    string RoutingKey = "",
    bool Durable = true
);