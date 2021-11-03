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
            bool isDefault = false)
        {
            var menu = context.Menus.SingleOrDefault(x => x.Type == type);

            return menu != null
                ? Update(context, text, canExpectInput, isDefault, menu)
                : Add(context, type, text, canExpectInput, isDefault);
        }

        private static Menu Add(
            ApplicationDbContext context,
            MenuType type,
            string text,
            bool canExpectInput,
            bool isDefault)
        {
            var menu = new Menu
            {
                Type = type,
                Text = text,
                CanExpectInput = canExpectInput,
                IsDefault = isDefault
            };

            context.Menus.Add(menu);
            context.SaveChanges();

            return menu;
        }

        private static Menu Update(
            ApplicationDbContext context,
            string text,
            bool canExpectInput,
            bool isDefault,
            Menu menu)
        {
            menu.Text = text;
            menu.CanExpectInput = canExpectInput;
            menu.IsDefault = isDefault;

            context.Menus.Update(menu);
            context.SaveChanges();

            return menu;
        }
    }
}