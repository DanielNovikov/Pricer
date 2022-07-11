using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;

public class TogglePriceGrowingCommandInitializer
{
	public static Command Initialize(Menu menu, Menu menuToRedirect)
	{
		var command = new Command(
			CommandKey.TogglePriceGrowing,
			ResourceKey.Command_TogglePriceGrowing,
			menuToRedirect);

		menu.Commands.Add(command);

		return command;
	}
}