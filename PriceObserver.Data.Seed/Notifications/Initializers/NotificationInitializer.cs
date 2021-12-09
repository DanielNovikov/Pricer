using System;
using System.Linq;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Seed.Notifications.Initializers
{
    public class NotificationInitializer
    {
        public static void Initialize(
            ApplicationDbContext context,
            string text,
            DateTime planned)
        {
            var notification = context.Notifications.SingleOrDefault(x => x.Planned == planned);

            if (notification is not null)
                Update(context, notification, text);
            else
                Add(context, text, planned);
        }

        private static void Update(ApplicationDbContext context, Notification notification, string text)
        {
            notification.Text = text;

            context.Update(notification);
            context.SaveChanges();
        }

        private static void Add(ApplicationDbContext context, string text, DateTime planned)
        {
            var notification = new Notification
            {
                Text = text,
                Planned = planned,
                Executed = false
            };

            context.Notifications.Add(notification);
            context.SaveChanges();
        }
    }
}