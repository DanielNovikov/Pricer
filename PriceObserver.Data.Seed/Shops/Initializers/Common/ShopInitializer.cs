using System;
using System.Linq;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Shops.Initializers.Common
{
    public class ShopInitializer
    {
        public static Shop Initialize(
            ApplicationDbContext context,
            string name,
            ShopType type,
            string host,
            Uri logoUrl)
        {
            var shop = context.Shops.SingleOrDefault(x => x.Type == type);

            return shop is not null 
                ? Update(context, shop, name, host, logoUrl) 
                : Add(context, name, type, host, logoUrl);
        }

        private static Shop Add(
            ApplicationDbContext context,
            string name,
            ShopType type,
            string host,
            Uri logoUrl)
        {
            var shop = new Shop
            {
                Type = type,
                Name = name,
                Host = host,
                LogoUrl = logoUrl
            };

            context.Shops.Update(shop);
            context.SaveChanges();

            return shop;
        }

        private static Shop Update(
            ApplicationDbContext context, 
            Shop shop, 
            string name,
            string host,
            Uri logoUrl)
        {
            shop.Name = name;
            shop.Host = host;
            shop.LogoUrl = logoUrl;

            context.Shops.Update(shop);
            context.SaveChanges();

            return shop;
        }
    }
}