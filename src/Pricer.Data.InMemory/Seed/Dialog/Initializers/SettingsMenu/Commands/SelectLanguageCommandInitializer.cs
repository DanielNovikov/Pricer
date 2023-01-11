using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;

public class SelectLanguageCommandInitializer
{
	public static Command Initialize(Menu menu)
	{
		var command = new Command(
			CommandKey.ChangeLanguage,
			ResourceKey.Command_SelectLanguage, 
			null);

		menu.Commands.Add(command);

		return command;
	}
}