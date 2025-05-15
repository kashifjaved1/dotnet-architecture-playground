using SCM.Core.Domain;
using SCM.Core.Events;
using SCM.Core.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Inventory.src.Domain
{
    public class InventoryItem : AggregateRoot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Sku { get; private set; }

        public int AvailableQuantity { get; private set; }
        public int ReservedQuantity { get; private set; }
        public int ReorderThreshold { get; private set; } = 10;

        // Required by EF Core
        private InventoryItem() { }

        public InventoryItem(Guid id, string sku, int initialStock)
        {
            Apply(new InventoryItemCreatedEvent(id, sku, initialStock));
        }

        protected override void When(InventoryItemCreatedEvent @event)
        {
            Id = @event.ItemId;
            Sku = @event.Sku;
            AvailableQuantity = @event.InitialStock;
        }

        protected override void When(StockAllocatedEvent @event)
        {
            AvailableQuantity -= @event.Quantity;
            ReservedQuantity += @event.Quantity;
        }

        public void AllocateStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Allocation quantity must be positive");

            if (AvailableQuantity < quantity)
                throw new InsufficientStockException(Sku, AvailableQuantity, quantity);

            AvailableQuantity -= quantity;
            ReservedQuantity += quantity;

            RaiseEvent(new StockAllocatedEvent(Id, quantity));

            CheckStockLevels();
        }

        public void ReleaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Release quantity must be positive");

            if (ReservedQuantity < quantity)
                throw new ArgumentException("Cannot release more than reserved");

            ReservedQuantity -= quantity;
            AvailableQuantity += quantity;
        }

        private void CheckStockLevels()
        {
            if (AvailableQuantity <= ReorderThreshold)
            {
                RaiseEvent(new LowStockEvent(Id, Sku, AvailableQuantity));
            }
        }
    }
}
