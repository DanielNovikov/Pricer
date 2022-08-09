using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Common.Extensions;

namespace PriceObserver.Background.Jobs;

public class ItemsObserver : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
        
    public ItemsObserver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

               await scope
                   .GetService<IItemsObserverService>()
                   .Observe();

                var delay = new Random().Next(60, 120);
                await Task.Delay(TimeSpan.FromMinutes(delay), cancellationToken);
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}