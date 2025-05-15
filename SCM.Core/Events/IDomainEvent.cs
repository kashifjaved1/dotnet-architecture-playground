namespace SCM.Core.Events
{
    public interface IDomainEvent
    {
        DateTime OccurredOn { get; }
    }
}
