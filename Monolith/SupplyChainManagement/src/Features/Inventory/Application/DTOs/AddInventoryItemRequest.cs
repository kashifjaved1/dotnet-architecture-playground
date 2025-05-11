namespace SupplyChainManagement.src.Features.Inventory.Application.DTOs
{
    public record AddInventoryItemRequest(string Sku, int AvailableQuantity, int ReservedQuantity);
}
