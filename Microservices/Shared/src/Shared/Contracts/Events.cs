namespace Shared.Contracts;

public record OrderCreatedEvent(Guid OrderId, Guid CustomerId, List<OrderItem> Items);
public record StockReservedEvent(Guid OrderId);
public record StockRejectedEvent(Guid OrderId, string Reason);
public record PaymentCompletedEvent(Guid OrderId);
public record PaymentFailedEvent(Guid OrderId, string Reason);
public record OrderShippedEvent(Guid OrderId);

public record OrderItem(Guid ProductId, int Quantity);