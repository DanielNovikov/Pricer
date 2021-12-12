using System.Linq;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Seed.Dialog.Initializers
{
    public class CommandInitializer
    {
        public static Command Initialize(
            ApplicationDbContext context,
            CommandType type,
            ResourceKey resourceKey,
            Menu menuToRedirect = null)
        {
            var command = context.Commands.SingleOrDefault(x => x.Type == type);

            var resource = context.Resources.Single(x => x.Key == resourceKey);
            
            return command is not null
                ? Update(context, command, resource, menuToRedirect)
                : Add(context, type, resource, menuToRedirect);
        }

        private static Command Add(
            ApplicationDbContext context,
            CommandType type,
            Resource resource,
            Menu menuToRedirect)
        {
            var command = new Command
            {
                Type = type,
                Resource = resource,
                MenuToRedirect = menuToRedirect
            };

            context.Commands.Add(command);
            context.SaveChanges();
            
            return command;
        }

        private static Command Update(
            ApplicationDbContext context,
            Command command,
            Resource resource,
            Menu menuToRedirect)
        {
            command.Resource = resource;
            command.MenuToRedirect = menuToRedirect;

            context.Commands.Update(command);
            context.SaveChanges();

            return command;
        }
    }
}