using SupplyChainManagement.src.Core.Domain.Events;
using SupplyChainManagement.src.Core.Interfaces;

namespace SupplyChainManagement.src.Features.Inventory.Infrastructure.Bus
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
        {
            var type = typeof(TEvent);
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Delegate>();

            _handlers[type].Add(handler);
        }

        public void Publish(IDomainEvent @event)
        {
            if (_handlers.TryGetValue(@event.GetType(), out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler.DynamicInvoke(@event);
                }
            }
        }
    }
}
