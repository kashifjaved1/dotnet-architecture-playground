namespace SupplyChainManagement.src.Inventory.Application.DTOs
{
    public record AddInventoryItemRequest(string Sku, int AvailableQuantity, int ReservedQuantity);
}
