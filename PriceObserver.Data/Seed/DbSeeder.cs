using PriceObserver.Data.Seed.AppNotifications;

namespace PriceObserver.Data.Seed;

public class DbSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        using var transaction = context.Database.BeginTransaction();

        try
        {
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