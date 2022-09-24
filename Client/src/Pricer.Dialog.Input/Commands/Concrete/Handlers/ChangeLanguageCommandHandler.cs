using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

public class ChangeLanguageCommandHandler : ICommandHandler
{
	private readonly ICallbackDataBuilder _callbackDataBuilder;
	private readonly IUserActionLogger _userActionLogger;

	public ChangeLanguageCommandHandler(
		ICallbackDataBuilder callbackDataBuilder,
		IUserActionLogger userActionLogger)
	{
		_callbackDataBuilder = callbackDataBuilder;
		_userActionLogger = userActionLogger;
	}

	public CommandKey Key => CommandKey.ChangeLanguage;

	public ValueTask<CommandHandlingServiceResult> Handle(User user)
	{
		_userActionLogger.LogCalledChangingLanguageMenu(user);	
		
		var language = user.SelectedLanguageKey == LanguageKey.Ukranian ? LanguageKey.Russian : LanguageKey.Ukranian;
		var callbackData = _callbackDataBuilder.BuildJson(CallbackKey.ChangeLanguage, language);

		var changeLanguageButton = new CallbackResourceButton(ResourceKey.Dialog_ChangeLanguage, callbackData);

		var keyboard = new MessageKeyboard(changeLanguageButton);
		
		var replyResource = user.SelectedLanguageKey == LanguageKey.Ukranian
			? ResourceKey.Dialog_ChangeLanguageToRussian
			: ResourceKey.Dialog_ChangeLanguageToUkrainian;

		var replyResult = new ReplyKeyboardResult(keyboard, replyResource);
		return ValueTask.FromResult(CommandHandlingServiceResult.Success(replyResult));
	}
}