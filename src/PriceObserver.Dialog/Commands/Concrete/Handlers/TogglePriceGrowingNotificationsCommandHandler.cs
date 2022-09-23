using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

public class TogglePriceGrowingNotificationsCommandHandler : ICommandHandler
{
	private readonly IUserActionLogger _userActionLogger;
	private readonly ICallbackDataBuilder _callbackDataBuilder;

	public TogglePriceGrowingNotificationsCommandHandler(
		IUserActionLogger userActionLogger,
		ICallbackDataBuilder callbackDataBuilder)
	{
		_userActionLogger = userActionLogger;
		_callbackDataBuilder = callbackDataBuilder;
	}

	public CommandKey Key => CommandKey.TogglePriceGrowingNotifications;

	public ValueTask<CommandHandlingServiceResult> Handle(User user)
	{
		_userActionLogger.LogCalledTogglingPriceGrowingMenu(user);

		var enableNotifications = !user.GrowthPriceNotificationsEnabled;
		var callbackData = _callbackDataBuilder.BuildJson(CallbackKey.TogglePriceGrowingNotifications, enableNotifications);

		var toggleNotificationsButtonTitle = enableNotifications
			? ResourceKey.Dialog_EnablePriceGrowingNotifications
			: ResourceKey.Dialog_DisablePriceGrowingNotifications; 
		
		var toggleNotificationsButton = new CallbackResourceButton(toggleNotificationsButtonTitle, callbackData);
		var keyboard = new MessageKeyboard(toggleNotificationsButton);
		
		var replyResource = enableNotifications
			? ResourceKey.Dialog_TogglePriceGrowingNotificationsToEnabled
			: ResourceKey.Dialog_TogglePriceGrowingNotificationsToDisabled;

		var replyResult = new ReplyKeyboardResult(keyboard, replyResource);
		return ValueTask.FromResult(CommandHandlingServiceResult.Success(replyResult));
		
	}
}