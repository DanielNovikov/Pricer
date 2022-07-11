using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.DisablePriceGrowingCommand;

public class DisablePriceGrowingCommandHandler : ICommandHandler
{
	private readonly IUserBackgroundSettingsService _userBackgroundSettingsService;

	public DisablePriceGrowingCommandHandler(IUserBackgroundSettingsService userBackgroundSettingsService)
	{
		_userBackgroundSettingsService = userBackgroundSettingsService;
	}

	public CommandKey Type => CommandKey.DisablePriceGrowing;

	public async Task<CommandHandlingServiceResult> Handle(User user)
	{
		var replyResult = await _userBackgroundSettingsService.SetPriceGrowthNotificationsEnabled(user, false);
		return CommandHandlingServiceResult.Success(replyResult);
	}
}