using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Telegram.Abstract;

namespace PriceObserver.Background.Services.Concrete;

public class AppNotificationService : IAppNotificationService
{
	private readonly IAppNotificationRepository _appNotificationRepository;
	private readonly IUserRepository _userRepository;
	private readonly ITelegramBotService _telegramBotService;
	private readonly IResourceService _resourceService;
	private readonly IUserLanguage _userLanguage;

	public AppNotificationService(
		IAppNotificationRepository appNotificationRepository,
		IUserRepository userRepository,
		ITelegramBotService telegramBotService,
		IResourceService resourceService,
		IUserLanguage userLanguage)
	{
		_appNotificationRepository = appNotificationRepository;
		_userRepository = userRepository;
		_telegramBotService = telegramBotService;
		_resourceService = resourceService;
		_userLanguage = userLanguage;
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
				_userLanguage.Set(user.SelectedLanguageKey);
				var content = _resourceService.Get(notification.Content);

				if (!string.IsNullOrEmpty(notification.VideoUrl))
					await _telegramBotService.SendVideo(user.ExternalId, notification.VideoUrl);
				
				await _telegramBotService.SendMessage(user.ExternalId, content);
			}

			notification.Executed = true;
			await _appNotificationRepository.Update(notification);
		}
	}
}