using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;

public class TogglePriceGrowingCommandInitializer
{
	public static Command Initialize(Menu menu)
	{
		var command = new Command(
			CommandKey.TogglePriceGrowingNotifications,
			ResourceKey.Command_TogglePriceGrowingNotifications,
			null);

		menu.Commands.Add(command);

		return command;
	}
}