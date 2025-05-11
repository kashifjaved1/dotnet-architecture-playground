using SupplyChainManagement.src.Inventory.Domain;

namespace SupplyChainManagement.src.Core.Interfaces
{
    public interface IInventoryRepository
    {
        InventoryItem GetById(Guid id);
        void Save(InventoryItem item);
    }
}
