using Shared.Contracts;
using Shared.Messaging;

namespace ShippingService.src.Consumers;

public class PaymentCompletedConsumer
{
    private readonly IEventBus _eventBus;

    public PaymentCompletedConsumer(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.Subscribe<PaymentCompletedEvent>(HandleAsync);
    }

    private async Task HandleAsync(PaymentCompletedEvent evt)
    {
        Console.WriteLine($"[Shipping] Received PaymentCompletedEvent for Order {evt.OrderId}");

        // Simulate shipping delay
        await Task.Delay(100);

        var shipped = new OrderShippedEvent(evt.OrderId);
        _eventBus.Publish(shipped);

        Console.WriteLine($"[Shipping] OrderShippedEvent published for Order {evt.OrderId}");
    }
}
