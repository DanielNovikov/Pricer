using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Background.Services.Abstract;

public interface IItemDeletionKeyboardBuilder
{
	MessageKeyboard Build(Item item);
}