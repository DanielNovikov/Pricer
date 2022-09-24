using System.Text.Json;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callback.Handlers.Abstract;
using Pricer.Dialog.Callback.Models;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;

namespace Pricer.Dialog.Callback.Handlers.Concrete;

public class TogglePriceGrowingNotificationsCallbackHandler : ICallbackHandler
{
	private readonly IUserActionLogger _userActionLogger;
	private readonly IUserService _userService;

	public TogglePriceGrowingNotificationsCallbackHandler(
		IUserActionLogger userActionLogger,
		IUserService userService)
	{
		_userActionLogger = userActionLogger;
		_userService = userService;
	}

	public CallbackKey Key => CallbackKey.TogglePriceGrowingNotifications;

	public async Task<CallbackHandlingResult> Handle(CallbackModel callback)
	{
		var enablePriceGrowingElement = (JsonElement)callback.Parameters[0];
		var enablePriceGrowing = enablePriceGrowingElement.GetBoolean();

		var user = callback.User;
			
		_userActionLogger.LogToggledPriceGrowthNotifications(user, enablePriceGrowing);
		await _userService.ChangePriceGrowthNotificationsEnabled(user, enablePriceGrowing);

		var resultMessage = enablePriceGrowing
			? ResourceKey.Dialog_PriceGrowingNotificationsEnabled
			: ResourceKey.Dialog_PriceGrowingNotificationsDisabled;
		
		var result = new ReplyResourceResult(resultMessage);
		return CallbackHandlingResult.Success(result);
	}
}