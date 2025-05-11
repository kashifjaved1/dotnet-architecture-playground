using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SupplyChainManagement.src.Core.Domain.Aggregates;
using SupplyChainManagement.src.Core.Domain.Events;
using SupplyChainManagement.src.Core.Domain.Exceptions;

namespace SupplyChainManagement.src.Features.Inventory.Domain
{
    public class InventoryItem : AggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; }
        public string Sku { get; } 
        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }

        public InventoryItem(string sku, int initialStock)
        {
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

        // Required by EF Core
        public InventoryItem() { }
    }
}
