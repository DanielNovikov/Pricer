using System.Linq;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.Common
{
    public class MenuInitializer
    {
        public static Menu Initialize(
            ApplicationDbContext context,
            MenuType type,
            string text,
            bool canExpectInput,
            bool isDefault = false,
            Menu parent = null)
        {
            var menu = context.Menus.SingleOrDefault(x => x.Type == type);

            return menu != null
                ? Update(context, menu, text, canExpectInput, isDefault, parent)
                : Add(context, type, text, canExpectInput, isDefault, parent);
        }

        private static Menu Add(
            ApplicationDbContext context,
            MenuType type,
            string text,
            bool canExpectInput,
            bool isDefault,
            Menu parent)
        {
            var menu = new Menu
            {
                Type = type,
                Text = text,
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
            string text,
            bool canExpectInput,
            bool isDefault,
            Menu parent)
        {
            menu.Text = text;
            menu.CanExpectInput = canExpectInput;
            menu.IsDefault = isDefault;
            menu.Parent = parent;

            context.Menus.Update(menu);
            context.SaveChanges();

            return menu;
        }
    }
}