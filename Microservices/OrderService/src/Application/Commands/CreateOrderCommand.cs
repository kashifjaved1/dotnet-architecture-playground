namespace OrderService.src.Application.Commands;

public record CreateOrderCommand(Guid CustomerId, List<OrderItemDto> Items);

public record OrderItemDto(Guid ProductId, int Quantity);