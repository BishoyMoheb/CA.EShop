using CA.EShop.Domain.Abstractions;
using MediatR;

namespace CA.EShop.Application.Abstractions.IMessaging
{
    public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvents
    {
    }
}
