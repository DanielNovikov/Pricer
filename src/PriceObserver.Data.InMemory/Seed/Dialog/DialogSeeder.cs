using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.Common;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SelectLanguageMenu.Commands;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SupportMenu;

namespace PriceObserver.Data.InMemory.Seed.Dialog;

public class DialogSeeder
{
	public static void Seed(IMemoryCache cache)
	{
		var selectLanguage = SelectLanguageMenuInitializer.Initialize();
		var homeMenu = HomeMenuInitializer.Initialize();
		var supportMenu = SupportMenuInitializer.Initialize(homeMenu);

		var menus = new List<Menu>
		{
			homeMenu,
			supportMenu,
			selectLanguage
		};

		cache.Set(CacheKey.Menus, menus);

		var helpCommand = HelpCommandInitializer.Initialize(homeMenu);
		var addCommand = AddCommandInitializer.Initialize(homeMenu);
		var allItemsCommand = AllItemsCommandInitializer.Initialize(homeMenu);
		var shopsCommand = ShopsCommandInitializer.Initialize(homeMenu);
		var websiteCommand = WebsiteCommandInitializer.Initialize(homeMenu);
		var writeToSupportCommand = WriteToSupportCommandInitializer.Initialize(homeMenu, supportMenu);

		var selectUkrainianLanguage = SelectUkrainianLanguageCommandInitializer.Initialize(selectLanguage);

		var selectRussianLanguage = SelectRussianLanguageCommandInitializer.Initialize(selectLanguage);

		var backCommand = BackCommandInitializer.Initialize();

		var commands = new List<Command>
		{
			helpCommand,
			addCommand,
			allItemsCommand,
			shopsCommand,
			websiteCommand,
			writeToSupportCommand,
			selectUkrainianLanguage,
			selectRussianLanguage,
			backCommand
		};

		cache.Set(CacheKey.Commands, commands);
	}
}