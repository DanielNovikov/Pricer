using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Common.Extensions;

namespace PriceObserver.Background.Jobs;

public class AppNotificationsSender : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AppNotificationsSender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(
            async () =>
            {
                using var scope = _serviceProvider.CreateScope();

                try
                {
                    var appNotificationService = scope.GetService<IAppNotificationService>();
                    await appNotificationService.Execute();
                }
                catch (Exception ex)
                {
                    var logger = scope.GetService<ILogger<AppNotificationsSender>>();
                    logger.LogError("Exception occured while sending notifications of type {1}", ex.GetType().FullName);
                }
            }, 
            cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}