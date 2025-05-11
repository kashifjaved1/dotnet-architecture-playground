namespace SupplyChainManagement.src.Inventory.Application.DTOs
{
    public record AllocationRequest(Guid ItemId, int Quantity);
}
