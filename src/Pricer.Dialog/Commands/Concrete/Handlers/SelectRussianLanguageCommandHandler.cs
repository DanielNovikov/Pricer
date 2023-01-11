using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class SelectRussianLanguageCommandHandler : ICommandHandler
{
	private readonly IUserLanguageChanger _userLanguageChanger;

	public SelectRussianLanguageCommandHandler(IUserLanguageChanger userLanguageChanger)
	{
		_userLanguageChanger = userLanguageChanger;
	}

	public CommandKey Key => CommandKey.SelectRussianLanguage;

	public async ValueTask<CommandHandlingServiceResult> Handle(User user)
	{
		var replyResult = await _userLanguageChanger.Change(user, LanguageKey.Russian);
		return CommandHandlingServiceResult.Success(replyResult);
	}
}