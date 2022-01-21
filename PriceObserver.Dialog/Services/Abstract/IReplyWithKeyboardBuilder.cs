using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IReplyWithKeyboardBuilder
{
    Task<ReplyResult> Build(Menu menu);
}