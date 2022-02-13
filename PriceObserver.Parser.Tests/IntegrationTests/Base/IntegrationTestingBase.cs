using System;
using Microsoft.Extensions.DependencyInjection;

namespace PriceObserver.Parser.Tests.IntegrationTests.Base;

public abstract class IntegrationTestingBase
{
    private readonly IServiceProvider _serviceProvider;
    
    protected IntegrationTestingBase()
    {
        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddParserServices();

        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    protected TService GetService<TService>()
    {
        return _serviceProvider.GetService<TService>() ??
               throw new InvalidOperationException($"Service {typeof(TService).FullName} could not be resolved");
    }
}