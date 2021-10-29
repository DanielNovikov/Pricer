using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Common;

namespace PriceObserver.Telegram.Dialog.Menu.Abstract
{
    public interface IReplyWithKeyboardBuilder
    {
        Task<ReplyResult> Build(Model.Data.Menu menu);
    }
}