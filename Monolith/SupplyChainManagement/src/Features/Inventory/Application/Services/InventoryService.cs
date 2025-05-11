using SupplyChainManagement.src.Core.Interfaces;
using SupplyChainManagement.src.Features.Inventory.Application.DTOs;
using SupplyChainManagement.src.Features.Inventory.Domain;

namespace SupplyChainManagement.src.Features.Inventory.Application.Services
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

        public void SaveStock(AddInventoryItemRequest request)
        {
            InventoryItem item = new(request.Sku, request.AvailableQuantity);

            _repository.Save(item);
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
