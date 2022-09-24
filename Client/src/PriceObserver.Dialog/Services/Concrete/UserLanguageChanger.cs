using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserLanguageChanger : IUserLanguageChanger
{
	private readonly IUserService _userService;
	private readonly IMenuRepository _menuRepository;
	private readonly IUserRedirectionService _userRedirectionService;
	private readonly IUserLanguage _userLanguage;
	private readonly IUserActionLogger _userActionLogger;

	public UserLanguageChanger(
		IUserService userService,
		IMenuRepository menuRepository,
		IUserRedirectionService userRedirectionService,
		IUserLanguage userLanguage,
		IUserActionLogger userActionLogger)
	{
		_userService = userService;
		_menuRepository = menuRepository;
		_userRedirectionService = userRedirectionService;
		_userLanguage = userLanguage;
		_userActionLogger = userActionLogger;
	}

	public async Task<IReplyResult> Change(User user, LanguageKey languageKey)
	{
		_userActionLogger.LogSelectedLanguage(user, languageKey);
		await _userService.ChangeSelectedLanguage(user, languageKey);
		_userLanguage.Set(languageKey);

		var homeMenu = _menuRepository.GetByKey(MenuKey.Home);
		return await _userRedirectionService.Redirect(user, homeMenu);
	}
}