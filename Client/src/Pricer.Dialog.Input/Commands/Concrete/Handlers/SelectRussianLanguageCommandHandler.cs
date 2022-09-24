using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

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