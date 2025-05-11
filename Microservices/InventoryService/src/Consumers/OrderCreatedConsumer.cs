using MassTransit;
using Shared.Contracts;

namespace InventoryService.src.Consumers
{
    public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var evt = context.Message;

            Console.WriteLine($"[Inventory] Received OrderCreatedEvent for Order {evt.OrderId}");

            // Simulate inventory check logic
            bool allInStock = evt.Items.All(item => item.Quantity <= 10);

            if (allInStock)
            {
                var reserved = new StockReservedEvent(evt.OrderId);
                await context.Publish(reserved);
                Console.WriteLine($"[Inventory] Stock reserved for Order {evt.OrderId}");
            }
            else
            {
                var rejected = new StockRejectedEvent(evt.OrderId, "Insufficient stock");
                await context.Publish(rejected);
                Console.WriteLine($"[Inventory] Stock rejected for Order {evt.OrderId}");
            }
        }
    }
}
