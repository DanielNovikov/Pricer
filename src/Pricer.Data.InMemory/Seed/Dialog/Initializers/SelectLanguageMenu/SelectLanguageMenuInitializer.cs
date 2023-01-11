using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu;

public class SelectLanguageMenuInitializer
{
	public static Menu Initialize()
	{
		return new Menu(MenuKey.SelectLanguage, ResourceKey.Menu_SelectLanguage, false, true, null);
	}
}