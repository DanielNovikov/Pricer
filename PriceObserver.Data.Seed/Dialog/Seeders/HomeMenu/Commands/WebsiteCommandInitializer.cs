﻿using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.HomeMenu.Commands
{
    public class WebsiteCommandInitializer
    {
        public static void Initialize(ApplicationDbContext context, Menu menu)
        {
            var websiteCommand = CommandInitializer.Initialize(
                context,
                CommandType.Website,
                ResourceKey.Command_Website);

            MenuCommandInitializer.Initialize(context, menu, websiteCommand);
        }
    }
}