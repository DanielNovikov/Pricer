using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.Jobs
{
    public class NotificationsJob : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public NotificationsJob(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var notificationRepository = scope.ServiceProvider.GetService<INotificationRepository>();
            var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
            var telegramBotService = scope.ServiceProvider.GetService<ITelegramBotService>();
            
            var notifications = await notificationRepository!.GetToExecute();

            if (!notifications.Any())
                return;
                
            var users = await userRepository!.GetAll();

            foreach (var notification in notifications)
            {
                foreach (var user in users)
                {
                    await telegramBotService!.SendMessage(user.Id, notification.Text);
                }

                notification.Executed = true;
                await notificationRepository.Update(notification);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}