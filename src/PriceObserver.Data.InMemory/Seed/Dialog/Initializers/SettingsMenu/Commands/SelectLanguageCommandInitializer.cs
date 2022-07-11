using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;

public class SelectLanguageCommandInitializer
{
	public static Command Initialize(Menu menu, Menu menuToRedirect)
	{
		var command = new Command(
			CommandKey.SelectLanguage,
			ResourceKey.Command_SelectLanguage,
			menuToRedirect);

		menu.Commands.Add(command);

		return command;
	}
}