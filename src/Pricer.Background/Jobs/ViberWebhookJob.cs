using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Common.Extensions;

namespace Pricer.Background.Jobs;

public class ViberWebhookJob : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public ViberWebhookJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(async () =>
        {
            using var scope = _serviceProvider.CreateScope();
            
            await Task.Delay(2000, cancellationToken);

            var viberBotService = scope.GetService<IViberBotService>();
            await viberBotService.SetWebhook();
        }, cancellationToken);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}