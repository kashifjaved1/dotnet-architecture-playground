using System;

namespace SCM.Order.src.Infrastructure.Sagas
{
    public class OrderSaga :
    IEventHandler<OrderCreatedEvent>,
    IEventHandler<InventoryReservedEvent>,
    IEventHandler<PaymentProcessedEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly IInventoryClient _inventoryClient;
        private readonly IPaymentClient _paymentClient;

        public async Task Handle(OrderCreatedEvent @event)
        {
            try
            {
                // Step 1: Reserve inventory
                await _inventoryClient.ReserveItems(@event.OrderId, @event.Items);
            }
            catch (Exception ex)
            {
                _eventBus.Publish(new OrderCancelledEvent(@event.OrderId, ex.Message));
            }
        }

        public async Task Handle(InventoryReservedEvent @event)
        {
            // Step 2: Process payment
            await _paymentClient.ProcessPayment(@event.OrderId);
        }

        public async Task Handle(PaymentProcessedEvent @event)
        {
            // Step 3: Finalize order
            _eventBus.Publish(new OrderCompletedEvent(@event.OrderId));
        }
    }
}
