using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Menus.Abstract;

namespace PriceObserver.Telegram.Dialog.Menus.Concrete
{
    public class ReplyWithKeyboardBuilder : IReplyWithKeyboardBuilder
    {
        private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;

        public ReplyWithKeyboardBuilder(IMenuKeyboardBuilder menuKeyboardBuilder)
        {
            _menuKeyboardBuilder = menuKeyboardBuilder;
        }

        public async Task<ReplyResult> Build(Menu menu)
        {
            var keyboard = await _menuKeyboardBuilder.Build(menu);
            
            return ReplyResult.ReplyWithKeyboard(menu.Text, keyboard);
        }
    }
}