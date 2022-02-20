using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IReplyWithKeyboardBuilder
{
    ReplyResult Build(Menu menu);
}