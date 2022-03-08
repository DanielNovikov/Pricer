using System;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Tests.Integration;

public abstract class IntegrationTestingBase
{
    protected readonly IParser Parser;
    
    protected IntegrationTestingBase()
    {
        var serviceProvider = new ServiceCollection()
            .AddParserServices()
            .BuildServiceProvider();

        Parser = serviceProvider.GetService<IParser>()
            ?? throw new ArgumentNullException(nameof(IParser));
    }
}