using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pricer.Dialog.FunctionalTests;

public abstract class IntegrationTestingBase
{
    protected static readonly IMessageHandler EntryPoint;

    private static IServiceProvider _serviceProvider = default!;
    
    static IntegrationTestingBase()
    {
        Initialize();
        
        EntryPoint = GetService<IMessageHandler>();
    }
    
    protected static TService GetService<TService>()
    {
        return _serviceProvider.GetService<TService>() ??
            throw new ArgumentNullException(typeof(TService).FullName);
    }

    protected static UpdateServiceModel BuildServiceModel(string text)
    {
        return new UpdateServiceModel(
            text,
            123456,
            "Danylo",
            "Novikov",
            "@DanyloNovikov");
    }
    
    private static void Initialize()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        _serviceProvider = new ServiceCollection()
            .AddInMemoryDataRepositories()
            .AddPersistentDataRepositories()
            .AddDataServices()
            .AddCommonServices()
            .AddParserServices()
            .AddDialogServices(configuration)
            .AddDbContext<ApplicationDbContext>(options => 
                options.UseInMemoryDatabase("Dialog.Tests"))
            .BuildServiceProvider();

        using var scope = _serviceProvider.CreateScope();

        var memoryCache = scope.GetService<IMemoryCache>();
        InMemorySeeder.Seed(memoryCache);
    }
}