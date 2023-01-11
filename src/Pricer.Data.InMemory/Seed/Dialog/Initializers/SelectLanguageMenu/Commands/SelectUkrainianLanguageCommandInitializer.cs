using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu.Commands;

public class SelectUkrainianLanguageCommandInitializer
{
	public static Command Initialize(Menu menu)
	{
		var command = new Command(
			CommandKey.SelectUkrainianLanguage,
			ResourceKey.Command_SelectUkrainianLanguage,
			null);

		menu.Commands.Add(command);

		return command;
	}
}