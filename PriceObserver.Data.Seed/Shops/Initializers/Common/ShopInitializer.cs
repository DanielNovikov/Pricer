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
            string host)
        {
            var shop = context.Shops.SingleOrDefault(x => x.Type == type);

            return shop != null 
                ? Update(context, shop, name, host) 
                : Add(context, name, type, host);
        }

        private static Shop Add(ApplicationDbContext context, string name, ShopType type, string host)
        {
            var shop = new Shop
            {
                Type = type,
                Name = name,
                Host = host
            };

            context.Shops.Update(shop);
            context.SaveChanges();

            return shop;
        }

        private static Shop Update(ApplicationDbContext context, Shop shop, string name, string host)
        {
            shop.Name = name;
            shop.Host = host;

            context.Shops.Update(shop);
            context.SaveChanges();

            return shop;
        }
    }
}