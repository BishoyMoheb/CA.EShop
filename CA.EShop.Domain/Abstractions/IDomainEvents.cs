using MediatR;
using System;

namespace CA.EShop.Domain.Abstractions
{
    public interface IDomainEvents : INotification
    {
       public Guid Id { get; init; }

        DateTime OccurredOnUtc { get; init; }
    }
}
