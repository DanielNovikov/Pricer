using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Callbacks.Abstract;

public interface IDeleteItemKeyboardBuilder
{
	MessageKeyboard Build(Item item);
}