using PriceObserver.Data.InMemory.Models.Enums;
using System.Linq;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Seed.AppNotifications.Initializers;

public class AppNotificationInitializer
{
    public static void Initialize(
        ApplicationDbContext context,
        ResourceKey content,
        string videoUrl)
    {
        var appNotification = context.AppNotifications
            .FirstOrDefault(x => x.Content == content && x.VideoUrl == videoUrl);
        
        if (appNotification is not null)
            return;

        appNotification = new AppNotification
        {
            Content = content,
            VideoUrl = videoUrl,
            Executed = false
        };

        context.AppNotifications.Add(appNotification);
        context.SaveChanges();
    }
}