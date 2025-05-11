namespace SupplyChainManagement.src.Core.Domain.Events
{
    public record LowStockEvent(Guid ItemId, int RemainingStock) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
