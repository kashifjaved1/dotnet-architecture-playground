using Microsoft.EntityFrameworkCore;
using SCM.Inventory.src.Domain;

namespace SCM.Inventory.src.Infrastructure
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { }

        public DbSet<InventoryItem> InventoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>(b =>
            {
                b.HasKey(i => i.Id);
                b.Property(i => i.Sku).IsRequired();
                b.HasData(SeedInventoryItems());
            });
        }

        private static List<InventoryItem> SeedInventoryItems() => new()
        {
            new InventoryItem("SKU-001", 100),
            new InventoryItem("SKU-002", 50)
        };
    }
}
