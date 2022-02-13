﻿using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PriceObserver.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddImplementations<TBase>(this IServiceCollection services)
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
    }
}