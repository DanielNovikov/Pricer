using System.Linq;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers.Common
{
    public class CommandInitializer
    {
        public static Command Initialize(
            ApplicationDbContext context,
            CommandType type,
            string title,
            Menu menuToRedirect = null)
        {
            var command = context.Commands.SingleOrDefault(x => x.Type == type);

            return command is not null
                ? Update(context, command, title, menuToRedirect)
                : Add(context, type, title, menuToRedirect);
        }

        private static Command Add(ApplicationDbContext context, CommandType type, string title, Menu menuToRedirect)
        {
            var command = new Command
            {
                Type = type,
                Title = title,
                MenuToRedirect = menuToRedirect
            };

            context.Commands.Add(command);
            context.SaveChanges();
            
            return command;
        }

        private static Command Update(ApplicationDbContext context, Command command, string title, Menu menuToRedirect)
        {
            command.Title = title;
            command.MenuToRedirect = menuToRedirect;

            context.Commands.Update(command);
            context.SaveChanges();

            return command;
        }
    }
}