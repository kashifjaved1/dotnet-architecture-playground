namespace SupplyChainManagement.src.Features.Inventory.Application.DTOs
{
    public record AllocationRequest(Guid ItemId, int Quantity);
}
