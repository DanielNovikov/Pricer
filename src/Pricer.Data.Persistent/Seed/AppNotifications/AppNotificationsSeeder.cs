using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Seed.AppNotifications.Initializers;

namespace Pricer.Data.Persistent.Seed.AppNotifications;

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