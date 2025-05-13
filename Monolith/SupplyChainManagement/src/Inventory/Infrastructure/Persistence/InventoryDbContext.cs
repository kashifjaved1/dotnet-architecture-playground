using Microsoft.EntityFrameworkCore;
using SupplyChainManagement.src.Inventory.Domain;

namespace SupplyChainManagement.src.Inventory.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { }

        public DbSet<InventoryItem>? InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>(b =>
            {
                b.HasKey(i => i.Id);
                b.Property(i => i.Sku).IsRequired();
            });
        }
    }
}
