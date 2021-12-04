using System;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Seed.Shops.Initializers.Common;

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
                new Uri("https://media-exp1.licdn.com/dms/image/C4E1BAQHdYvrPFaAN-A/company-background_10000/0/1616762724843?e=2159024400&v=beta&t=N8-gqYABqVA8ZJ9R5T5Po3w2b2orNFNneL1x4kZ2ufM"));
            
            ShopInitializer.Initialize(
                context,
                "Answear",
                ShopType.Answear,
                "answear.ua",
                new Uri("https://i1.wp.com/expert.com.ua/wp-content/uploads/2015/11/logo-2.png?fit=605%2C225&ssl=1"));
            
            ShopInitializer.Initialize(
                context,
                "Brocard",
                ShopType.Brocard,
                "www.brocard.ua",
                new Uri("https://cosmo-multimall.com/wp-content/uploads/2021/03/078.jpg"));
            
            ShopInitializer.Initialize(
                context,
                "FARFETCH",
                ShopType.Farfetch,
                "www.farfetch.com",
                new Uri("https://logos-download.com/wp-content/uploads/2016/12/Farfetch_logo_logotype.png"));
        }
    }
}