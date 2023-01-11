using System.Threading.Tasks;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

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