using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.src.Core.Factories;
using SupplyChainManagement.src.Inventory.Domain;

namespace SupplyChainManagement.src.Inventory.Infrastructure.Persistence
{
    public class InventoryDbContextFactory : DbContextFactory<InventoryDbContext> { }
}