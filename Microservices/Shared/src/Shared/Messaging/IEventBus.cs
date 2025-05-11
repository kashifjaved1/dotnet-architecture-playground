namespace Shared.Messaging;

public interface IEventBus
{
    void Publish<T>(T @event) where T : class;
    void Subscribe<T>(Func<T, Task> handler) where T : class;
}