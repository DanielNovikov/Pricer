using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Callbacks.Concrete.Handlers;

public class SelectLanguageCallbackHandler : ICallbackHandler
{
	private readonly IUserActionLogger _userActionLogger;
	private readonly IUserService _userService;
	private readonly IUserLanguage _userLanguage;
	private readonly IResourceService _resourceService;
	private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;

	public SelectLanguageCallbackHandler(
		IUserActionLogger userActionLogger,
		IUserService userService,
		IUserLanguage userLanguage,
		IResourceService resourceService,
		IMenuKeyboardBuilder menuKeyboardBuilder)
	{
		_userActionLogger = userActionLogger;
		_userService = userService;
		_userLanguage = userLanguage;
		_resourceService = resourceService;
		_menuKeyboardBuilder = menuKeyboardBuilder;
	}

	public CallbackKey Key => CallbackKey.ChangeLanguage; 

	public async Task<CallbackHandlingResult> Handle(CallbackModel callback)
	{
		var languageKeyElement = (JsonElement)callback.Parameters[0];
		var languageKey = (LanguageKey)languageKeyElement.GetInt32();

		var user = callback.User;
			
		_userActionLogger.LogSelectedLanguage(user, languageKey);
		await _userService.ChangeSelectedLanguage(user, languageKey);
		_userLanguage.Set(languageKey);

		var menuKeyboard = _menuKeyboardBuilder.Build(user.MenuKey);
		
		var result = new ReplyKeyboardResult(menuKeyboard, ResourceKey.Dialog_LanguageChanged);
		return CallbackHandlingResult.Success(result);
	}
}