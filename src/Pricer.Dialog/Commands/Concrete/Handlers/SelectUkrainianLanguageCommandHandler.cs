using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class SelectUkrainianLanguageCommandHandler : ICommandHandler
{
	private readonly IUserLanguageChanger _userLanguageChanger;

	public SelectUkrainianLanguageCommandHandler(IUserLanguageChanger userLanguageChanger)
	{
		_userLanguageChanger = userLanguageChanger;
	}

	public CommandKey Key => CommandKey.SelectUkrainianLanguage;

	public async ValueTask<CommandHandlingServiceResult> Handle(User user)
	{
		var replyResult = await _userLanguageChanger.Change(user, LanguageKey.Ukranian);
		return CommandHandlingServiceResult.Success(replyResult);
	}
}