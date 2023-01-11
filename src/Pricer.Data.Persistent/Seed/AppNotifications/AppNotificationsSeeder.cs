using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Seed.AppNotifications.Initializers;

namespace PriceObserver.Data.Persistent.Seed.AppNotifications;

public class AppNotificationsSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        AppNotificationInitializer.Initialize(
            context,
            ResourceKey.AppNotification_HowToAddItem,
            "https://pricer.ink/videos/how-to-add-item.mp4");
    }
}