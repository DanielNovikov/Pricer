using System.Linq;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.DataSeed
{
    public class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            if (context.Menus.Any())
                return;
            
            var homeMenu = new Menu
            {
                Text = "Выберите что хотите сделать ⬇",
                Type = MenuType.Home,
                CanExpectInput = false,
                IsDefault = true
            };

            context.Menus.Add(homeMenu);
                
            var newItemMenu = new Menu
            {
                Text = "Вставьте ссылку на желаемый товар 🆕",
                Type = MenuType.NewItem,
                CanExpectInput = true,
                IsDefault = false
            };

            context.Menus.Add(newItemMenu);
            

            var addCommand = new Command
            {
                Title = "Добавить ➕",
                Type = CommandType.Add,
                MenuToRedirect = newItemMenu
            };

            context.Commands.Add(addCommand);
            
            var websiteCommand = new Command
            {
                Title = "Сайт 🌍",
                Type = CommandType.Website
            };
            
            context.Commands.Add(websiteCommand);
            
            var backToHomeCommand = new Command
            {
                Title = "Назад ◀",
                Type = CommandType.BackToHome,
                MenuToRedirect = homeMenu
            };

            context.Commands.Add(backToHomeCommand);


            var homeAddMenuCommand = new MenuCommand
            {
                Menu = homeMenu,
                Command = addCommand
            };

            context.MenuCommands.Add(homeAddMenuCommand);
            
            var homeWebsiteMenuCommand = new MenuCommand
            {
                Menu = homeMenu,
                Command = websiteCommand
            };
            
            context.MenuCommands.Add(homeWebsiteMenuCommand);

            var newItemBackMenuCommand = new MenuCommand
            {
                Menu = newItemMenu,
                Command = backToHomeCommand
            };
            
            context.MenuCommands.Add(newItemBackMenuCommand);

            
            var intertopShop = new Shop
            {
                Name = "Intertop",
                Type = ShopType.Intertop,
                Host = "intertop.ua"
            };

            context.Shops.Add(intertopShop);
            
            var mdFashionShop = new Shop
            {
                Name = "MdFashion",
                Type = ShopType.MdFashion,
                Host = "md-fashion.com.ua"
            }; 
            
            context.Shops.Add(mdFashionShop);
            
            context.SaveChanges();
            context.DetachAll();
        }
    }
}