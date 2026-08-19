using CA.EShop.Domain.Shared;
using MediatR;

namespace CA.EShop.Application.Abstractions.IMessaging
{
    public interface ICommandHandler<in TCommand> 
        : IRequestHandler<TCommand, GenResult>
        where TCommand : ICommand
    {
    }

    public interface ICommandHandler<in TCommand, TResponse>
        : IRequestHandler<TCommand, TResult<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
