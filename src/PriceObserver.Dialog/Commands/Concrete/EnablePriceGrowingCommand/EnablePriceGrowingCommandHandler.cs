using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.EnablePriceGrowingCommand;

public class EnablePriceGrowingCommandHandler : ICommandHandler
{
	private readonly IUserBackgroundSettingsService _userBackgroundSettingsService;

	public EnablePriceGrowingCommandHandler(IUserBackgroundSettingsService userBackgroundSettingsService)
	{
		_userBackgroundSettingsService = userBackgroundSettingsService;
	}

	public CommandKey Type => CommandKey.EnablePriceGrowing;

	public async Task<CommandHandlingServiceResult> Handle(User user)
	{
		var replyResult = await _userBackgroundSettingsService.SetPriceGrowthNotificationsEnabled(user, true);
		return CommandHandlingServiceResult.Success(replyResult);
	}
}