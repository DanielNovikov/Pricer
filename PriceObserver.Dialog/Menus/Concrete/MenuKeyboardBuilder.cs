﻿using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Menus.Concrete
{
    public class MenuKeyboardBuilder : IMenuKeyboardBuilder
    {
        private readonly IMenuCommandRepository _menuCommandRepository;
        private readonly ICommandRepository _commandRepository;

        private const int ButtonsInRow = 2; 
        
        public MenuKeyboardBuilder(
            IMenuCommandRepository menuCommandRepository,
            ICommandRepository commandRepository)
        {
            _menuCommandRepository = menuCommandRepository;
            _commandRepository = commandRepository;
        }

        public async Task<MenuKeyboard> Build(Menu menu)
        {
            var commands = await _menuCommandRepository.GetCommandsByMenuId(menu.Id);

            if (menu.ParentId.HasValue)
            {
                var backCommand = await _commandRepository.GetByType(CommandType.Back);
                commands = commands.Append(backCommand).ToList();
            }
            
            var buttonTitles = commands.Select((x, i) => new { Index = i, Text = x.Resource.Value });

            var buttonRows = buttonTitles
                .GroupBy(x => x.Index / ButtonsInRow)
                .Select(x => x.Select(y => y.Text));

            var buttonsGrid = buttonRows
                .Select(
                    x => x
                        .Select(y => new MenuKeyboardButton(y))
                        .ToList())
                .ToList();

            return new MenuKeyboard(buttonsGrid);
        }
    }
}