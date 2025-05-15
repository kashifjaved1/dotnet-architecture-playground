namespace SCM.Core.Events
{
    public record InventoryItemCreatedEvent(
    Guid ItemId,
    string Sku,
    int InitialStock) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
