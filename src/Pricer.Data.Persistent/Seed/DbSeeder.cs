using Pricer.Data.Persistent.Seed.AppNotifications;

namespace Pricer.Data.Persistent.Seed;

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