using System;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.Seed.Dialog;
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
                
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
        }
    }
}