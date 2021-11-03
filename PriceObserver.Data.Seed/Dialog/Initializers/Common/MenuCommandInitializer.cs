﻿using System.Linq;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Seed.Dialog.Initializers.Common
{
    public class MenuCommandInitializer
    {
        public static MenuCommand Initialize(
            ApplicationDbContext context,
            Menu menu,
            Command command)
        {
            var menuCommand = context.MenuCommands
                .SingleOrDefault(x => 
                    x.MenuId == menu.Id && 
                    x.CommandId == command.Id);

            if (menuCommand != null)
                return menuCommand;

            menuCommand = new MenuCommand
            {
                Menu = menu,
                Command = command
            };

            context.MenuCommands.Add(menuCommand);
            context.SaveChanges();

            return menuCommand;
        }
    }
}