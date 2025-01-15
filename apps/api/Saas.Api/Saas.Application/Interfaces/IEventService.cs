namespace Saas.Application.Interfaces;

public interface IEventService
{
    Task PublishAsync<T>(T eventInfo);
    void Subscribe<T>(Func<T, Task> asyncCallback);
}