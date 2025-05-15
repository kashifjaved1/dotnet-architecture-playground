using SCM.Core.Domain;

namespace SCM.Order.src.Domain
{
    public class Order : AggregateRoot
    {
        public Guid Id { get; }
        public OrderStatus Status { get; private set; }
        private readonly List<OrderItem> _items = new();

        public void CreateOrder(List<OrderItem> items)
        {
            _items.AddRange(items);
            Status = OrderStatus.Created;
            RaiseEvent(new OrderCreatedEvent(Id, items));
        }

        public void MarkAsPaid()
        {
            Status = OrderStatus.Paid;
            RaiseEvent(new OrderPaidEvent(Id));
        }
    }
}
