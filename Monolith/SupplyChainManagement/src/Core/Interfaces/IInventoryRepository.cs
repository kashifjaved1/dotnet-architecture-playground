using SupplyChainManagement.src.Features.Inventory.Domain;

namespace SupplyChainManagement.src.Core.Interfaces
{
    public interface IInventoryRepository
    {
        InventoryItem GetById(Guid id);
        void Save(InventoryItem item);
    }
}
