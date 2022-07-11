using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.TogglePriceGrowingMenu.Commands;

public class EnablePriceGrowingCommand
{
	public static Command Initialize(Menu menu)
	{
		var command = new Command(
			CommandKey.EnablePriceGrowing,
			ResourceKey.Command_EnablePriceGrowing,
			null);

		menu.Commands.Add(command);

		return command;
	}
}