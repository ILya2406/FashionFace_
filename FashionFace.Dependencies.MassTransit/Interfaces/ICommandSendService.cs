using System.Threading.Tasks;

namespace FashionFace.Dependencies.MassTransit.Interfaces;

public interface ICommandSendService
{
    Task SendAsync<TCommand>(
        TCommand command
    ) where TCommand : class;
}