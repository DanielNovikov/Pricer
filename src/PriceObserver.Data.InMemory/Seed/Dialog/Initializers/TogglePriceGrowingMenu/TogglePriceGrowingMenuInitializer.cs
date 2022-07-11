using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.TogglePriceGrowingMenu;

public class TogglePriceGrowingMenuInitializer
{
	public static Menu Initialize(Menu parent)
	{
		return new Menu(MenuKey.TogglePriceGrowing, ResourceKey.Menu_TogglePriceGrowing, false, false, parent);
	}
}