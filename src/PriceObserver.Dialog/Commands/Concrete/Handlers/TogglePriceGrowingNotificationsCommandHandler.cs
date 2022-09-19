using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

public class TogglePriceGrowingNotificationsCommandHandler : ICommandHandler
{
	private readonly IUserActionLogger _userActionLogger;
	private readonly ICallbackDataBuilder _callbackDataBuilder;
	private readonly IResourceService _resourceService;

	public TogglePriceGrowingNotificationsCommandHandler(
		IUserActionLogger userActionLogger,
		ICallbackDataBuilder callbackDataBuilder,
		IResourceService resourceService)
	{
		_userActionLogger = userActionLogger;
		_callbackDataBuilder = callbackDataBuilder;
		_resourceService = resourceService;
	}

	public CommandKey Key => CommandKey.TogglePriceGrowingNotifications;

	public Task<CommandHandlingServiceResult> Handle(User user)
	{
		_userActionLogger.LogCalledTogglingPriceGrowingMenu(user);

		var enableNotifications = !user.GrowthPriceNotificationsEnabled;
		var callbackData = _callbackDataBuilder.BuildJson(CallbackKey.TogglePriceGrowingNotifications, enableNotifications);

		var toggleNotificationsButtonTitle = enableNotifications
			? ResourceKey.Dialog_EnablePriceGrowingNotifications
			: ResourceKey.Dialog_DisablePriceGrowingNotifications; 
		
		var toggleNotificationsButton = new CallbackResourceButton(toggleNotificationsButtonTitle, callbackData);
		var keyboard = new MessageKeyboard(toggleNotificationsButton);
		
		var replyMessageKey = enableNotifications
			? ResourceKey.Dialog_TogglePriceGrowingNotificationsToEnabled
			: ResourceKey.Dialog_TogglePriceGrowingNotificationsToDisabled;

		var replyMessage = _resourceService.Get(replyMessageKey);

		var replyResult = ReplyResult.ReplyWithMessageKeyboard(replyMessage, keyboard);
		return Task.FromResult(CommandHandlingServiceResult.Success(replyResult));
		
	}
}