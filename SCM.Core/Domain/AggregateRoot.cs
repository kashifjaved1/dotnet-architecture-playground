using SCM.Core.Events;

namespace SCM.Core.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void RaiseEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void Apply(IDomainEvent @event)
        {
            // Pattern matching for event application
            switch (@event)
            {
                case InventoryItemCreatedEvent created:
                    When(created);
                    break;
                case StockAllocatedEvent allocated:
                    When(allocated);
                    break;
                    // Add other event types as needed
            }
        }

        // Event handlers
        protected virtual void When(InventoryItemCreatedEvent @event) { }
        protected virtual void When(StockAllocatedEvent @event) { }
    }
}
