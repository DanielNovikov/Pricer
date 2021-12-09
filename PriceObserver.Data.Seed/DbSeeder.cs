using PriceObserver.Data.Seed.Dialog;
using PriceObserver.Data.Seed.Notifications;
using PriceObserver.Data.Seed.Resources;
using PriceObserver.Data.Seed.Shops;

namespace PriceObserver.Data.Seed
{
    public class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                DialogSeeder.Seed(context);
                ShopsSeeder.Seed(context);
                NotificationsSeeder.Seed(context);
                ResourcesSeeder.Seed(context);
                
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}