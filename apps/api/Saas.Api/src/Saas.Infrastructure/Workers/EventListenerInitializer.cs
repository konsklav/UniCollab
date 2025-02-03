using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Saas.Application.Interfaces;
using Saas.Infrastructure.Common;

namespace Saas.Infrastructure.Workers;

internal sealed class EventListenerInitializer : IHostedService
{
    public EventListenerInitializer(Assembly assembly, IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<EventListenerInitializer>>();
        
        logger.LogInformation("Initializing {listenerInterface} implementations of assembly {assemblyName}",
            nameof(IEventListener),
            assembly.Location);
        
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