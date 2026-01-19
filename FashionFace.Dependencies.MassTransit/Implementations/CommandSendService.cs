using System;
using System.Threading.Tasks;

using FashionFace.Dependencies.MassTransit.Interfaces;

using MassTransit;

namespace FashionFace.Dependencies.MassTransit.Implementations;

public sealed class CommandSendService(
    ISendEndpointProvider sendEndpointProvider
) : ICommandSendService
{
    public async Task SendAsync<TCommand>(
        TCommand command
    ) where TCommand : class
    {
        var endpointNameFormatter =
            KebabCaseEndpointNameFormatter.Instance;

        var queueName =
            endpointNameFormatter.Message<TCommand>();

        var uri =
            new Uri(
                $"queue:{queueName}"
            );

        var endpoint =
            await
                sendEndpointProvider
                    .GetSendEndpoint(
                        uri
                    );

        await
            endpoint
                .Send(
                    command
                );
    }
}