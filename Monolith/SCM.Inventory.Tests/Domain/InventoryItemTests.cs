using SupplyChainManagement.src.Core.Domain.Exceptions;
using SupplyChainManagement.src.Inventory.Domain;

namespace SCM.Inventory.Tests.Domain
{
    public class InventoryItemTests
    {
        [Fact]
        public void AllocateStock_UpdatesQuantities()
        {
            // Arrange
            var item = new InventoryItem("SKU-001", 100);

            // Act
            item.AllocateStock(30);

            // Assert
            Assert.Equal(70, item.AvailableQuantity);
            Assert.Equal(30, item.ReservedQuantity);
        }

        [Fact]
        public void AllocateStock_ThrowsWhenInsufficient()
        {
            var item = new InventoryItem("SKU-002", 50);
            Assert.Throws<InsufficientStockException>(() => item.AllocateStock(60));
        }
    }
}