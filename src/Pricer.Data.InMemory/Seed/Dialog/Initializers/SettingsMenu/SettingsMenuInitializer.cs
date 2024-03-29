﻿using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Seed.Dialog.Initializers.SettingsMenu;

public class SettingsMenuInitializer
{
	public static Menu Initialize(Menu parent)
	{
		return new Menu(MenuKey.Settings, ResourceKey.Menu_Settings, false, false, parent);
	}
}