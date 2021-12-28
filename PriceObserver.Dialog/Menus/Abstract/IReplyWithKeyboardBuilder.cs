using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Menus.Abstract;

public interface IReplyWithKeyboardBuilder
{
    Task<ReplyResult> Build(Menu menu);
}