using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.EShop.Domain.Abstractions
{
    /// <summary>
    /// Represents the abstract domain event primitive.
    /// </summary>
    public abstract record RDomainEvent : IDomainEvents
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="occurredOnUtc">The occurred on date and time.</param>
        protected RDomainEvent(Guid id, DateTime occurredOnUTC) : this()
        {
            Id = id;
            OccurredOnUtc = occurredOnUTC;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RDomainEvent"/> class.
        /// </summary>
        private RDomainEvent()
        {
        }

        public Guid Id { get; init; }

        public DateTime OccurredOnUtc { get; init; }
    }
}
