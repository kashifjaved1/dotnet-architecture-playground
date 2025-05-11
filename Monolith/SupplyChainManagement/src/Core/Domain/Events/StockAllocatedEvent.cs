namespace SupplyChainManagement.src.Core.Domain.Events
{
    public record StockAllocatedEvent(Guid ItemId, int Quantity) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
