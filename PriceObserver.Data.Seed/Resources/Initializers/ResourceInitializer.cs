using System.Linq;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Seed.Resources.Initializers
{
    public class ResourceInitializer
    {   
        public static void Initialize(
            ApplicationDbContext context,
            ResourceKey key,
            string value)
        {
            var resource = context.Resources.SingleOrDefault(x => x.Key == key);

            if (resource == null)
                Add(context, key, value);
            else
                Update(context, resource, value);
        }

        private static void Add(
            ApplicationDbContext context,
            ResourceKey key,
            string value)
        {
            var resource = new Resource()
            {
                Key = key,
                Value = value
            };

            context.Resources.Update(resource);
            context.SaveChanges();
        }

        private static void Update(
            ApplicationDbContext context, 
            Resource resource, 
            string value)
        {
            resource.Value = value;

            context.Resources.Update(resource);
            context.SaveChanges();
        }
    }
}