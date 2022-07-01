using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IReplyWithKeyboardBuilder
{
    ReplyResult Build(Menu menu);
}