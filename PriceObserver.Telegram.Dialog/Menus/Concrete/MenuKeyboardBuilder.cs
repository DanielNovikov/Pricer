using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Dialog.Menus.Concrete
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

        public async Task<ReplyKeyboardMarkup> Build(Menu menu)
        {
            var commands = await _menuCommandRepository.GetCommandsByMenuId(menu.Id);

            if (menu.ParentId.HasValue)
            {
                var backCommand = await _commandRepository.GetByType(CommandType.Back);
                commands.Add(backCommand);
            }
            
            var buttonTitles = commands.Select((x, i) => new { Index = i, x.Title });

            var buttonRows = buttonTitles
                .GroupBy(x => x.Index / ButtonsInRow)
                .Select(x => x.Select(y => y.Title));

            var keyboardButtons = buttonRows
                .Select(x => x.Select(y => new KeyboardButton(y)));

            return new ReplyKeyboardMarkup(keyboardButtons, true);
        }
    }
}