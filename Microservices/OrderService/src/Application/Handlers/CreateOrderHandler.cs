using OrderService.src.Application.Commands;
using OrderService.src.Domain.Models;
using OrderService.src.Infrastructure;
using Shared.Contracts;
using Shared.Messaging;
using OrderItem = OrderService.src.Domain.Models.OrderItem;

namespace OrderService.src.Application.Handlers;

public class CreateOrderHandler
{
    private readonly IEventBus _eventBus;
    private readonly OrderDbContext _db;

    public CreateOrderHandler(IEventBus eventBus, OrderDbContext db)
    {
        _eventBus = eventBus;
        _db = db;
    }

    public async Task<Guid> HandleAsync(CreateOrderCommand command)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = command.CustomerId,
            Items = command.Items.Select(i => new OrderItem { ProductId = i.ProductId, Quantity = i.Quantity }).ToList(),
            Status = OrderStatus.Created
        };

        await _db.Orders.AddAsync(order);
        await _db.SaveChangesAsync();

        var orderCreated = new OrderCreatedEvent(order.Id, order.CustomerId,
            order.Items.Select(i => new Shared.Contracts.OrderItem(i.ProductId, i.Quantity)).ToList());

        _eventBus.Publish(orderCreated);

        return order.Id;
    }
}