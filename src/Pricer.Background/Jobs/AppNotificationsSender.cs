using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pricer.Background.Services.Abstract;
using Pricer.Common.Extensions;

namespace Pricer.Background.Jobs;

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
                    await scope
                        .GetService<IAppNotificationService>()
                        .Execute();
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