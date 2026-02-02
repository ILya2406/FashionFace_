using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.MassTransit.Interfaces;

using MassTransit;

namespace FashionFace.Dependencies.MassTransit.Implementations;

public sealed class EventPublishService(
    IPublishEndpoint publishEndpoint
) : IEventPublishService
{
    public Task PublishAsync<TEvent>(
        TEvent @event
    ) where TEvent : class =>
     publishEndpoint
        .Publish(
            @event
        );
}