﻿using PriceObserver.Data.Persistent.Seed.AppNotifications.Initializers;

namespace PriceObserver.Data.Persistent.Seed.AppNotifications;

public class AppNotificationsSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        AppNotificationInitializer.Initialize(
            context,
            @"📋 Добавлены новые магазины электроники
- Rozetka (rozetka.com.ua)
- Citrus (www.ctrs.com.ua)
- Stylus (stylus.ua)
- eStore (estore.ua)");
    }
}