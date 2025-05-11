using SupplyChainManagement.src.Core.Domain.Events;

namespace SupplyChainManagement.src.Core.Domain.Aggregates
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _events = new();
        public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

        protected void RaiseEvent(IDomainEvent @event) => _events.Add(@event);
        public void ClearEvents() => _events.Clear();
    }
}
