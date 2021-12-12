using System.Linq;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers
{
    public class MenuInitializer
    {
        public static Menu Initialize(
            ApplicationDbContext context,
            MenuType type,
            ResourceKey resourceKey,
            bool canExpectInput,
            bool isDefault = false,
            Menu parent = null)
        {
            var menu = context.Menus.SingleOrDefault(x => x.Type == type);

            return menu is not null
                ? Update(context, menu, resourceKey, canExpectInput, isDefault, parent)
                : Add(context, type, resourceKey, canExpectInput, isDefault, parent);
        }

        private static Menu Add(
            ApplicationDbContext context,
            MenuType type,
            ResourceKey resourceKey,
            bool canExpectInput,
            bool isDefault,
            Menu parent)
        {
            var menu = new Menu
            {
                Type = type,
                ResourceKey = resourceKey,
                CanExpectInput = canExpectInput,
                IsDefault = isDefault,
                Parent = parent
            };

            context.Menus.Add(menu);
            context.SaveChanges();

            return menu;
        }

        private static Menu Update(
            ApplicationDbContext context,
            Menu menu,
            ResourceKey resourceKey,
            bool canExpectInput,
            bool isDefault,
            Menu parent)
        {
            menu.ResourceKey = resourceKey;
            menu.CanExpectInput = canExpectInput;
            menu.IsDefault = isDefault;
            menu.Parent = parent;

            context.Menus.Update(menu);
            context.SaveChanges();

            return menu;
        }
    }
}