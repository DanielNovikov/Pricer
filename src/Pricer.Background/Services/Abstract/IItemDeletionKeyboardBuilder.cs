using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models;

namespace Pricer.Background.Services.Abstract;

public interface IItemDeletionKeyboardBuilder
{
	MessageKeyboard Build(Item item);
}