using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Menu.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Dialog.Menu.Concrete
{
    public class ReplyWithKeyboardBuilder : IReplyWithKeyboardBuilder
    {
        private readonly IMenuCommandRepository _menuCommandRepository;

        private const int ButtonsInRow = 2; 
        
        public ReplyWithKeyboardBuilder(IMenuCommandRepository menuCommandRepository)
        {
            _menuCommandRepository = menuCommandRepository;
        }

        public async Task<ReplyResult> Build(Model.Data.Menu menu)
        {
            var commands = await _menuCommandRepository.GetMenuCommands(menu.Id);

            var buttonTitles = commands.Select((x, i) => new { Index = i, x.Title });

            var buttonRows = buttonTitles
                .GroupBy(x => x.Index / ButtonsInRow)
                .Select(x => x.Select(y => y.Title));

            var keyboardButtons = buttonRows
                .Select(x => x.Select(y => new KeyboardButton(y)));

            var keyboardMarkup = new ReplyKeyboardMarkup(keyboardButtons, true);
            
            return ReplyResult.ReplyWithKeyboard(menu.Text, keyboardMarkup);
        }
    }
}