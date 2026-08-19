using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Abstractions
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<IDomainEvents> ROColI_DEventsI { get; }
        void Clear_DomainEvents();
    }
}
