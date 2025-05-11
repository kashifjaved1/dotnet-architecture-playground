using PaymentService.src.Domain.Models;
using Shared.Contracts;
using Shared.Messaging;

namespace PaymentService.src.Consumers;

public class StockReservedConsumer
{
    private readonly IEventBus _eventBus;

    public StockReservedConsumer(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.Subscribe<StockReservedEvent>(HandleAsync);
    }

    private async Task HandleAsync(StockReservedEvent evt)
    {
        Console.WriteLine($"[Payment] Received StockReservedEvent for Order {evt.OrderId}");

        // Simulate payment process
        var result = SimulatePayment();

        if (result == PaymentResult.Success)
        {
            var completed = new PaymentCompletedEvent(evt.OrderId);
            _eventBus.Publish(completed);
            Console.WriteLine($"[Payment] Payment completed for Order {evt.OrderId}");
        }
        else
        {
            var failed = new PaymentFailedEvent(evt.OrderId, "Credit card declined");
            _eventBus.Publish(failed);
            Console.WriteLine($"[Payment] Payment failed for Order {evt.OrderId}");
        }

        await Task.CompletedTask;
    }

    private PaymentResult SimulatePayment()
    {
        // Fake 80% success rate
        return Random.Shared.Next(0, 100) < 80 ? PaymentResult.Success : PaymentResult.Failed;
    }
}
