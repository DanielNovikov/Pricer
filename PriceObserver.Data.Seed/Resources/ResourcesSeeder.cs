using PriceObserver.Data.Seed.Resources.Seeders;

namespace PriceObserver.Data.Seed.Resources
{
    public class ResourcesSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            DialogSeeder.Seed(context);
            UserActionSeeder.Seed(context);
            ParserSeeder.Seed(context);
            BackgroundSeeder.Seed(context);
            MenuSeeder.Seed(context);
        }
    }
}