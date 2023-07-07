using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Seed.Dialog.Initializers.Common;
using Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu;
using Pricer.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;
using Pricer.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu;
using Pricer.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu.Commands;
using Pricer.Data.InMemory.Seed.Dialog.Initializers.SupportMenu;

namespace Pricer.Data.InMemory.Seed.Dialog;

public class DialogSeeder
{
	public static void Seed(IMemoryCache cache)
	{
		var homeMenu = HomeMenuInitializer.Initialize();
		var supportMenu = SupportMenuInitializer.Initialize(homeMenu);
		var settingsMenu = SettingsMenuInitializer.Initialize(homeMenu);

		var menus = new List<Menu>
		{
			homeMenu,
			supportMenu,
			settingsMenu
		};

		cache.Set(CacheKey.Menus, menus);

		var helpCommand = HelpCommandInitializer.Initialize(homeMenu);
		var addCommand = AddCommandInitializer.Initialize(homeMenu);
		var allItemsCommand = AllItemsCommandInitializer.Initialize(homeMenu);
		var shopsCommand = ShopsCommandInitializer.Initialize(homeMenu);
		var websiteCommand = WebsiteCommandInitializer.Initialize(homeMenu);
		var writeToSupportCommand = WriteToSupportCommandInitializer.Initialize(homeMenu, supportMenu);
		var settingsCommand = SettingsCommandInitializer.Initialize(homeMenu, settingsMenu);

		var selectLanguageCommand = SelectLanguageCommandInitializer.Initialize(settingsMenu);
		var togglePriceGrowingCommand = TogglePriceGrowingCommandInitializer.Initialize(settingsMenu);
		var changeMinimumDiscountCommand = ChangeMinimumDiscountCommandInitializer.Initialize(settingsMenu);
		
		var backCommand = BackCommandInitializer.Initialize();

		var commands = new List<Command>
		{
			helpCommand,
			addCommand,
			allItemsCommand,
			shopsCommand,
			websiteCommand,
			writeToSupportCommand,
			settingsCommand,
			selectLanguageCommand,
			togglePriceGrowingCommand,
			backCommand,
			changeMinimumDiscountCommand
		};

		cache.Set(CacheKey.Commands, commands);
	}
}