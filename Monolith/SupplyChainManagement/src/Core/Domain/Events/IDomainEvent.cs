namespace SupplyChainManagement.src.Core.Domain.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
