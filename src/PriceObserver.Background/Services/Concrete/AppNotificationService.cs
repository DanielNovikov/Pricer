using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Telegram.Abstract;

namespace PriceObserver.Background.Services.Concrete;

public class AppNotificationService : IAppNotificationService
{
    private readonly IAppNotificationRepository _appNotificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITelegramBotService _telegramBotService;

    public AppNotificationService(
        IAppNotificationRepository appNotificationRepository,
        IUserRepository userRepository,
        ITelegramBotService telegramBotService)
    {
        _appNotificationRepository = appNotificationRepository;
        _userRepository = userRepository;
        _telegramBotService = telegramBotService;
    }

    public async Task Execute()
    {
        var notifications = await _appNotificationRepository.GetToExecute();
        if (!notifications.Any())
            return;
                
        var users = await _userRepository.GetAllActive();

        foreach (var notification in notifications)
        {
            foreach (var user in users)
            {
                await _telegramBotService.SendMessage(user.ExternalId, notification.Text);
            }

            notification.Executed = true;
            await _appNotificationRepository.Update(notification);
        }
    }
}