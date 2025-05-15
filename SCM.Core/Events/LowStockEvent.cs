namespace SCM.Core.Events
{
    public record LowStockEvent(
    Guid ItemId,
    string Sku,
    int RemainingStock) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
