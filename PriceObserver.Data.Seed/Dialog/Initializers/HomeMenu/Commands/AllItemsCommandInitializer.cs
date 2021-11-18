﻿using PriceObserver.Data.Models;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;
using CommandType = PriceObserver.Data.Models.Enums.CommandType;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class AllItemsCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu)
        {
            var allItemsCommand = CommandInitializer.Initialize(
                context,
                CommandType.AllItems,
                "Все товары ℹ");

            MenuCommandInitializer.Initialize(context, menu, allItemsCommand);

            return allItemsCommand;
        }
    }
}