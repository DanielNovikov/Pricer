using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserBackgroundSettingsService : IUserBackgroundSettingsService
{
	private readonly IUserActionLogger _userActionLogger;
	private readonly IUserService _userService;
	private readonly IMenuRepository _menuRepository;
	private readonly IUserRedirectionService _userRedirectionService;

	public UserBackgroundSettingsService(
		IUserActionLogger userActionLogger,
		IUserService userService,
		IMenuRepository menuRepository,
		IUserRedirectionService userRedirectionService)
	{
		_userActionLogger = userActionLogger;
		_userService = userService;
		_menuRepository = menuRepository;
		_userRedirectionService = userRedirectionService;
	}

	public async Task<ReplyResult> SetPriceGrowthNotificationsEnabled(User user, bool enabled)
	{
		_userActionLogger.LogToggledPriceGrowthNotifications(user, enabled);
		await _userService.ChangePriceGrowthNotificationsEnabled(user, enabled);

		var homeMenu = _menuRepository.GetByKey(MenuKey.Home);
		return await _userRedirectionService.Redirect(user, homeMenu);
	}
}