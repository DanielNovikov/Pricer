using System;
using Microsoft.Extensions.DependencyInjection;
using Pricer.Parser.Abstract;

namespace Pricer.Parser.FunctionalTests;

public abstract class TestBase
{
    protected readonly IParser Parser;
    
    protected TestBase()
    {
        var serviceProvider = new ServiceCollection()
            .AddParserServices()
            .BuildServiceProvider();

        Parser = serviceProvider.GetService<IParser>()
            ?? throw new ArgumentNullException(nameof(IParser));
    }
}