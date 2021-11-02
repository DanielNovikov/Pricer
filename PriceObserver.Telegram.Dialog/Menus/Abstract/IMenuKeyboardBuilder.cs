using System.Threading.Tasks;
using PriceObserver.Model.Data;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Dialog.Menus.Abstract
{
    public interface IMenuKeyboardBuilder
    {
        Task<ReplyKeyboardMarkup> Build(Menu menu);
    }
}