using SupplyChainManagement.src.Core.Domain.Events;

namespace SupplyChainManagement.src.Core.Interfaces
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent;
        void Publish(IDomainEvent @event);
    }
}
