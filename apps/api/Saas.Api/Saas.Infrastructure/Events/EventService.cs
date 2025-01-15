using System.Collections.Concurrent;
using Saas.Application.Interfaces;

namespace Saas.Infrastructure.Events;

internal class EventService : IEventService
{
    private readonly ConcurrentDictionary<Type, List<Delegate>> _listeners = [];
    
    public async Task PublishAsync<T>(T eventInfo)
    {
        var eventType = typeof(T);
        if (!_listeners.TryGetValue(eventType, out var eventListeners))
            return;

        foreach (var callback in eventListeners)
        {
            var func = (Func<T, Task>)callback;
            await func.Invoke(eventInfo);
        }
    }

    public void Subscribe<T>(Func<T, Task> asyncCallback)
    {
        var eventType = typeof(T);
        var eventListeners = _listeners.GetOrAdd(eventType, []);

        eventListeners.Add(asyncCallback);
    }
}