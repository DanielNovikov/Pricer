using PriceObserver.Data.Seed.Shops.Initializers.Common;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Shops
{
    public class ShopsSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            ShopInitializer.Initialize(
                context,
                "Intertop",
                ShopType.Intertop,
                "intertop.ua");
            
            ShopInitializer.Initialize(
                context,
                "MdFashion",
                ShopType.MdFashion,
                "md-fashion.com.ua");
            
            ShopInitializer.Initialize(
                context,
                "Answear",
                ShopType.Answear,
                "answear.ua");
        }
    }
}