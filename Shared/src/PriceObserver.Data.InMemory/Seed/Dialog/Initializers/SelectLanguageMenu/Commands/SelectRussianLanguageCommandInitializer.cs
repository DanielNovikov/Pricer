using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu.Commands;

public class SelectRussianLanguageCommandInitializer
{
	public static Command Initialize(Menu menu)
	{
		var command = new Command(
			CommandKey.SelectRussianLanguage,
			ResourceKey.Command_SelectRussianLanguage,
			null);

		menu.Commands.Add(command);

		return command;
	}
}