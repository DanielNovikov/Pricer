using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Telegram.Dialog.Menus.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Dialog.Menus.Concrete
{
    public class MenuKeyboardBuilder : IMenuKeyboardBuilder
    {
        private readonly IMenuCommandRepository _menuCommandRepository;

        private const int ButtonsInRow = 2; 
        
        public MenuKeyboardBuilder(IMenuCommandRepository menuCommandRepository)
        {
            _menuCommandRepository = menuCommandRepository;
        }

        public async Task<ReplyKeyboardMarkup> Build(Menu menu)
        {
            var commands = await _menuCommandRepository.GetMenuCommands(menu.Id);

            var buttonTitles = commands.Select((x, i) => new { Index = i, x.Title });

            var buttonRows = buttonTitles
                .GroupBy(x => x.Index / ButtonsInRow)
                .Select(x => x.Select(y => y.Title));

            var keyboardButtons = buttonRows
                .Select(x => x.Select(y => new KeyboardButton(y)));

            return new ReplyKeyboardMarkup(keyboardButtons);
        }
    }
}