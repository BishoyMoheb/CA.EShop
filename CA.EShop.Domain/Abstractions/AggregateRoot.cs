using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Abstractions
{
    /* Milan way of writting */
    public abstract class AggregateRoot : IAggregateRoot
    {
        private readonly List<IDomainEvents> _l_DomainEventsI = new();

        protected AggregateRoot()
        {
        }

        public IReadOnlyCollection<IDomainEvents> ROColI_DEventsI 
            =>_l_DomainEventsI.ToList().AsReadOnly();

        public void Clear_DomainEvents() 
            => _l_DomainEventsI.Clear();

        protected void Raise_DomainEvent(IDomainEvents domainEventsI) 
            => _l_DomainEventsI.Add(domainEventsI);
    }
}
