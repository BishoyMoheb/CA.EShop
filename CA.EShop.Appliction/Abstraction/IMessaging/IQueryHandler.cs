using CA.EShop.Domain.Shared;
using MediatR;

namespace CA.EShop.Application.Abstractions.IMessaging
{
    /// <summary>
    /// Represents the query handler interface.
    /// </summary>
    /// <typeparam name="TQuery">The query type.</typeparam>
    /// <typeparam name="TResponse">The query response type.</typeparam>
    public interface IQueryHandler<in TQuery, TResponse>
        : IRequestHandler<TQuery, TResult<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
