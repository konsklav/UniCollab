using System.Reflection;

namespace Saas.Infrastructure.Common;

internal static class ReflectionHelpers
{
    public static void ForEachImplementationOf<TInterface>(
        Assembly assembly,
        Action<Type> action)
    {
        var interfaceType = typeof(TInterface);
        if (!interfaceType.IsInterface)
            throw new InvalidOperationException("An interface type is required.");

        var implementations = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } &&
                        interfaceType.IsAssignableFrom(t));

        foreach (var implementation in implementations)
        {
            action(implementation);
        }
    }
}