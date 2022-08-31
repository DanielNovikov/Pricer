using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

public class ChangeLanguageCommandHandler : ICommandHandler
{
	private readonly ICallbackDataBuilder _callbackDataBuilder;
	private readonly IResourceService _resourceService;
	private readonly IUserActionLogger _userActionLogger;

	public ChangeLanguageCommandHandler(
		ICallbackDataBuilder callbackDataBuilder,
		IResourceService resourceService,
		IUserActionLogger userActionLogger)
	{
		_callbackDataBuilder = callbackDataBuilder;
		_resourceService = resourceService;
		_userActionLogger = userActionLogger;
	}

	public CommandKey Key => CommandKey.ChangeLanguage;

	public Task<CommandHandlingServiceResult> Handle(User user)
	{
		_userActionLogger.LogCalledChangingLanguageMenu(user);	
		
		var language = user.SelectedLanguageKey == LanguageKey.Ukranian ? LanguageKey.Russian : LanguageKey.Ukranian;
		var callbackData = _callbackDataBuilder.BuildJson(CallbackKey.ChangeLanguage, language);

		var changeLanguageButton = new CallbackKeyboardButton(ResourceKey.Dialog_ChangeLanguage, callbackData);

		var keyboard = new MessageKeyboard(changeLanguageButton);
		
		var replyMessageKey = user.SelectedLanguageKey == LanguageKey.Ukranian
			? ResourceKey.Dialog_ChangeLanguageToRussian
			: ResourceKey.Dialog_ChangeLanguageToUkrainian;

		var replyMessage = _resourceService.Get(replyMessageKey);

		var replyResult = ReplyResult.ReplyWithMessageKeyboard(replyMessage, keyboard);
		return Task.FromResult(CommandHandlingServiceResult.Success(replyResult));
	}
}