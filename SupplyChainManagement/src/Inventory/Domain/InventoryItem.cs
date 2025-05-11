using SupplyChainManagement.src.Core.Domain.Aggregates;
using SupplyChainManagement.src.Core.Domain.Events;
using SupplyChainManagement.src.Core.Domain.Exceptions;

namespace SupplyChainManagement.src.Inventory.Domain
{
    public class InventoryItem : AggregateRoot
    {
        public Guid Id { get; }
        public string Sku { get; }
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }

        public InventoryItem(Guid id, string sku, int initialStock)
        {
            Id = id;
            Sku = sku;
            AvailableQuantity = initialStock;
        }

        public void AllocateStock(int quantity)
        {
            if (AvailableQuantity < quantity)
            {
                throw new InsufficientStockException(Sku, AvailableQuantity, quantity);
            }

            AvailableQuantity -= quantity;
            ReservedQuantity += quantity;

            RaiseEvent(new StockAllocatedEvent(Id, quantity));

            if (AvailableQuantity <= 50)
            {
                RaiseEvent(new LowStockEvent(Id, AvailableQuantity));
            }
        }
    }
}
