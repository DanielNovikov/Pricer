﻿using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.Common;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.HomeMenu.Commands;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.NewItemMenu;
using PriceObserver.Data.InMemory.Seed.Dialog.Initializers.SupportMenu;

namespace PriceObserver.Data.InMemory.Seed.Dialog;

public class DialogSeeder
{
    public static void Seed(IMemoryCache cache)
    {
        var homeMenu = HomeMenuInitializer.Initialize();
        var newItemMenu = NewItemMenuInitializer.Initialize(homeMenu);
        var supportMenu = SupportMenuInitializer.Initialize(homeMenu);

        cache.Set(nameof(Menu), new List<Menu>
        {
            homeMenu,
            newItemMenu,
            supportMenu
        });
            
        var helpCommand = HelpCommandInitializer.Initialize(homeMenu);
        var addCommand = AddCommandInitializer.Initialize(homeMenu, newItemMenu);
        var allItemsCommand = AllItemsCommandInitializer.Initialize(homeMenu);
        var shopsCommand = ShopsCommandInitializer.Initialize(homeMenu);
        var websiteCommand = WebsiteCommandInitializer.Initialize(homeMenu);
        var writeToSupportCommand = WriteToSupportCommand.Initialize(homeMenu, supportMenu);

        var backCommand = BackCommandInitializer.Initialize();
            
        cache.Set(nameof(Command), new List<Command>
        {
            helpCommand,
            addCommand,
            allItemsCommand,
            shopsCommand,
            websiteCommand,
            writeToSupportCommand,
            backCommand
        });
    }
}