using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Saas.Application.Interfaces;
using Saas.Infrastructure.Common;

namespace Saas.Infrastructure.Workers;

/// <summary>
/// This background worker service uses reflection to initialize all implementations of <see cref="IEventListener"/>
/// of a specified <see cref="Assembly"/>.
/// </summary>
internal sealed class EventListenerInitializer : IHostedService
{
    public EventListenerInitializer(Assembly assembly, IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<EventListenerInitializer>>();
        ReflectionHelpers.ForEachImplementationOf<IEventListener>(
            assembly, 
            type =>
            {
                logger.LogInformation("Initializing listener: {listenerName}", type.FullName);
                var service = (IEventListener) serviceProvider.GetRequiredService(type);
                service.InitializeSubscriptions();
            });
    }
    
    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}