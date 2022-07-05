using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu;

public class SelectLanguageMenuInitializer
{
	public static Menu Initialize()
	{
		return new Menu(MenuKey.SelectLanguage, ResourceKey.Menu_SelectLanguage, false, true, null);
	}
}