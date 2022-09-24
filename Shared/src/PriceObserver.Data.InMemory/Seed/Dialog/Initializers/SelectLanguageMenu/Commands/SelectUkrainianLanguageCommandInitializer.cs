using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu.Commands;

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