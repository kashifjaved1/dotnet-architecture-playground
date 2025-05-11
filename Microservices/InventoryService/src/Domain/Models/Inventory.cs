namespace InventoryService.src.Domain.Models;

public class Inventory
{
    public Guid ProductId { get; set; }
    public int AvailableQuantity { get; set; }
}
