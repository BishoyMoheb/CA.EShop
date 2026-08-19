using CA.EShop.Domain.Shared;
using MediatR;

namespace CA.EShop.Application.Abstractions.IMessaging
{
    public interface IQuery<TResponse> : IRequest<TResult<TResponse>>
    {
    }
}
