using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pricer.Common.Extensions;
using Pricer.Data.InMemory.Seed;
using Pricer.Data.Persistent;

namespace Pricer.Background.Jobs;

public class SeedJob : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public SeedJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        
        var context = scope.GetService<ApplicationDbContext>();
        await context.Database.MigrateAsync(cancellationToken);
        
        var cache = scope.GetService<IMemoryCache>();
        InMemorySeeder.Seed(cache);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}