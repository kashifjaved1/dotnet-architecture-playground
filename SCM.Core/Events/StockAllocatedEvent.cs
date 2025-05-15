namespace SCM.Core.Events
{
    public record StockAllocatedEvent(
    Guid ItemId,
    int Quantity) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
