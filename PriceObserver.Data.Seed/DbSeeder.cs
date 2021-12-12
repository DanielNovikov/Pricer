using PriceObserver.Data.Seed.AppNotifications;
using PriceObserver.Data.Seed.Dialog;
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
                ResourcesSeeder.Seed(context);
                DialogSeeder.Seed(context);
                ShopsSeeder.Seed(context);
                AppNotificationsSeeder.Seed(context);
                
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