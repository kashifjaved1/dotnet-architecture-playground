namespace SupplyChainManagement.src.Core.Interfaces
{
    public interface IInventoryService
    {
        void AllocateStock(Guid itemId, int quantity);
    }
}
