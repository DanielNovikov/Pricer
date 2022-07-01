﻿using System;
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

                var itemsPriceService = scope.GetService<IItemsObserverService>();
                await itemsPriceService.Observe();
                    
                await Task.Delay(TimeSpan.FromHours(2), cancellationToken);
            }
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}