using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.TogglePriceGrowingMenu.Commands;

public class DisablePriceGrowingCommand
{
	public static Command Initialize(Menu menu)
	{
		var command = new Command(
			CommandKey.DisablePriceGrowing,
			ResourceKey.Command_DisablePriceGrowing,
			null);

		menu.Commands.Add(command);

		return command;
	}
}