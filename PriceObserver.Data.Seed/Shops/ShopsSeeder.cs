using System;
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
                "intertop.ua",
                new Uri("https://intertop.ua/upload/iblock/5e9/5e9af99fddb96796799d9a1523ce61ab.png"));
            
            ShopInitializer.Initialize(
                context,
                "MdFashion",
                ShopType.MdFashion,
                "md-fashion.com.ua",
                new Uri("https://md-fashion.com.ua/assets/4aa76fa0/logo.svg"));
            
            ShopInitializer.Initialize(
                context,
                "Answear",
                ShopType.Answear,
                "answear.ua",
                new Uri("https://i1.wp.com/expert.com.ua/wp-content/uploads/2015/11/logo-2.png?fit=605%2C225&ssl=1"));
        }
    }
}