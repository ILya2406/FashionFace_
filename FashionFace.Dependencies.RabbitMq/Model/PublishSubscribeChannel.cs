using System;

using RabbitMQ.Client;

namespace FashionFace.Dependencies.RabbitMq.Model;

public sealed record PublishSubscribeChannel(
    IChannel Channel,
    string Exchange,
    string Queue
) :
    IDisposable
{
    public void Dispose() =>
        Channel.Dispose();
}