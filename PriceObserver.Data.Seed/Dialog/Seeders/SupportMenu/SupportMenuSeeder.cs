﻿using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Dialog.Initializers;

namespace PriceObserver.Data.Seed.Dialog.Seeders.SupportMenu
{
    public class SupportMenuSeeder
    {
        public static Menu Seed(ApplicationDbContext context, Menu parent)
        {
            return MenuInitializer.Initialize(
                context,
                MenuType.Support,
                ResourceKey.Menu_Support,
                true,
                parent: parent);
        }
    }
}