namespace Ordering.Domain.Abstractions
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>

    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domEvent)
        {
            _domainEvents.Add(domEvent);
        }
        
        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] deqEvents = _domainEvents.ToArray();
            _domainEvents.Clear();

            return deqEvents;
        }
    }
}
