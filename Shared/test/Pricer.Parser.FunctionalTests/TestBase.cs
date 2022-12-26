using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Pricer.Parser.Abstract;
using Pricer.Parser.Concrete;
using Pricer.Parser.Options;

namespace Pricer.Parser.FunctionalTests;

public abstract class TestBase
{
    protected readonly IParser Parser;
    
    protected TestBase()
    {
        var configurationSection = Mock.Of<IConfigurationSection>();
        var configuration = Mock.Of<IConfiguration>(x => x.GetSection(nameof(ProxySettings)) == configurationSection);
        
        var serviceCollection = new ServiceCollection().AddParserServices(configuration);

        serviceCollection.RemoveAll(typeof(IProxyHttpClient));
        serviceCollection.AddHttpClient<IProxyHttpClient, ProxyHttpClient>();
        
        var serviceProvider = serviceCollection
            .BuildServiceProvider();

        Parser = serviceProvider.GetService<IParser>()
            ?? throw new ArgumentNullException(nameof(IParser));
    }
}