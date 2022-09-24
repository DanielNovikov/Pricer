using System.Linq;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Persistent.Seed.AppNotifications.Initializers;

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