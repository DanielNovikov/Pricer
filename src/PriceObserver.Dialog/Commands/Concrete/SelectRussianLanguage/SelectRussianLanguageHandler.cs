using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.SelectRussianLanguage;

public class SelectRussianLanguageHandler : ICommandHandler
{
	private readonly IUserLanguageChanger _userLanguageChanger;

	public SelectRussianLanguageHandler(IUserLanguageChanger userLanguageChanger)
	{
		_userLanguageChanger = userLanguageChanger;
	}

	public CommandKey Type => CommandKey.SelectRussianLanguage;

	public async Task<CommandHandlingServiceResult> Handle(User user)
	{
		var replyResult = await _userLanguageChanger.Change(user, LanguageKey.Russian);
		return CommandHandlingServiceResult.Success(replyResult);
	}
}