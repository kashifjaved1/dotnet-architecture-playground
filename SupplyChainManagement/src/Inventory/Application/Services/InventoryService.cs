using SupplyChainManagement.src.Core.Interfaces;

namespace SupplyChainManagement.src.Inventory.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly IEventBus _eventBus;

        public InventoryService(IInventoryRepository repository, IEventBus eventBus)
        {
            _repository = repository;
            _eventBus = eventBus;
        }

        public void AllocateStock(Guid itemId, int quantity)
        {
            var item = _repository.GetById(itemId);
            item.AllocateStock(quantity);

            foreach (var evt in item.Events)
            {
                _eventBus.Publish(evt);
            }

            _repository.Save(item);
        }
    }
}
