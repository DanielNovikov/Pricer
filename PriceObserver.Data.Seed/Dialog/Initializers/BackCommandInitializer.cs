﻿using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers
{
    public class BackCommandInitializer
    {
        public static Command Initialize(ApplicationDbContext context)
        {
            return CommandInitializer.Initialize(
                context,
                CommandType.Back,
                ResourceKey.Command_Back);
        }
    }
}