using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Pricer.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddImplementations<TBase>(this IServiceCollection services)
    {
        var baseType = typeof(TBase);

        var implementations = Assembly
            .GetCallingAssembly()
            .DefinedTypes
            .Where(type => 
                baseType.IsAssignableFrom(type) && 
                baseType != type && 
                !type.IsAbstract)
            .ToList();

        implementations.ForEach(implementation =>
        {
            services.AddTransient(baseType, implementation);
        });

        return services;
    }
}