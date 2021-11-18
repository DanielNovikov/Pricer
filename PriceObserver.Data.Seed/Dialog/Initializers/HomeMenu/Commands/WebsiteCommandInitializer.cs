﻿using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers.Common;

namespace PriceObserver.Data.Seed.Dialog.Initializers.HomeMenu.Commands
{
    public class WebsiteCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context, Menu menu)
        {
            var websiteCommand = CommandInitializer.Initialize(
                context,
                CommandType.Website,
                "Сайт 🌍");

            MenuCommandInitializer.Initialize(context, menu, websiteCommand);

            return websiteCommand;
        }
    }
}