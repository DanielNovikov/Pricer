using System;
using PriceObserver.Data.Seed.Notifications.Initializers.Common;

namespace PriceObserver.Data.Seed.Notifications
{
    public class NotificationsSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            NotificationInitializer.Initialize(
                context,
                @"🆕 Доступен новый магазин <b>Brocard</b>❕
Нажми <a href='https://www.brocard.ua'>здесь</a> что-бы перейти и поделится желаемыми товарами ➕",
                DateTime.Parse("2021-12-02 15:03:00").ToUniversalTime());
            
            NotificationInitializer.Initialize(
                context,
                @"🆕 Доступен новый магазин <b>FARFETCH</b>❕
Теперь дорогие вещи могут стать доступнее 💰",
                DateTime.Parse("2021-12-04 10:00:00").ToUniversalTime());
        }
    }
}