using System;
using Microsoft.Extensions.DependencyInjection;

namespace Pricer.Common.Extensions;

public static class ServiceScopeExtensions
{
    public static T GetService<T>(this IServiceScope scope)
    {
        return scope.ServiceProvider.GetService<T>() ?? 
               throw new ArgumentNullException(typeof(T).Name);
    }
}