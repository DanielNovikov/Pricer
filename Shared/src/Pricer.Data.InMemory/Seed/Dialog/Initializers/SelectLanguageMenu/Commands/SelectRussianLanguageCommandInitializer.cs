using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu.Commands;

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