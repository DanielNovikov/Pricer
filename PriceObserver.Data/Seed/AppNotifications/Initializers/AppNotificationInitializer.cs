using System.Linq;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Seed.AppNotifications.Initializers;

public class AppNotificationInitializer
{
    public static void Initialize(
        ApplicationDbContext context,
        string text)
    {
        var appNotification = context.AppNotifications.SingleOrDefault(x => x.Text == text);
        
        if (appNotification is not null)
            return;

        appNotification = new AppNotification
        {
            Text = text,
            Executed = false
        };

        context.AppNotifications.Add(appNotification);
        context.SaveChanges();
    }
}