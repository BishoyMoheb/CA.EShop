using CA.EShop.Domain.Shared;
using MediatR;

namespace CA.EShop.Application.Abstractions.IMessaging
{
    public interface ICommand : IRequest<GenResult>
    {
    }

    public interface ICommand<TResponse> : IRequest<TResult<TResponse>>
    {
    }

}
