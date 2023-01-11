using Pricer.Data.Persistent.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Background.Services.Abstract;

public interface IItemDeletionKeyboardBuilder
{
	InlineKeyboardMarkup Build(Item item);
}