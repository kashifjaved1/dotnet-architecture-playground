using SupplyChainManagement.src.Core.Interfaces;
using SupplyChainManagement.src.Features.Inventory.Domain;

namespace SupplyChainManagement.src.Features.Inventory.Infrastructure.Persistence
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryDbContext _context;

        public InventoryRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public InventoryItem GetById(Guid id) => _context.InventoryItems.FirstOrDefault(i => i.Id == id);

        public void Save(InventoryItem item)
        {
            var existing = _context.InventoryItems.Find(item.Id);
            if (existing == null)
                _context.Add(item);
            else
                _context.Entry(existing).CurrentValues.SetValues(item);

            _context.SaveChanges();
        }
    }
}
