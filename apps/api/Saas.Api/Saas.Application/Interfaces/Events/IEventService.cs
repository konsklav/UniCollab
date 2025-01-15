namespace Saas.Application.Interfaces.Events;

public interface IEventService
{
    Task PublishAsync<T>(T eventInfo);
    void Subscribe<T>(Func<T, Task> asyncCallback);
}