using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Common;

namespace PriceObserver.Telegram.Dialog.Menus.Abstract
{
    public interface IReplyWithKeyboardBuilder
    {
        Task<ReplyResult> Build(Menu menu);
    }
}