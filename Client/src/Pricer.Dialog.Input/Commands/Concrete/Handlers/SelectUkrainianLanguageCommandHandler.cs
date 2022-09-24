using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

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