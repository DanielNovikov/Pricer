﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Common.Extensions;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.Jobs;

public class AppNotificationsSender : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AppNotificationsSender(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var appNotificationRepository = scope.GetService<IAppNotificationRepository>();
        var userRepository = scope.GetService<IUserRepository>();
        var telegramBotService = scope.GetService<ITelegramBotService>();
            
        var notifications = await appNotificationRepository.GetToExecute();

        if (!notifications.Any())
            return;
                
        var users = await userRepository.GetAllActive();

        foreach (var notification in notifications)
        {
            foreach (var user in users)
            {
                await telegramBotService.SendMessage(user.ExternalId, notification.Text);
            }

            notification.Executed = true;
            await appNotificationRepository.Update(notification);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}