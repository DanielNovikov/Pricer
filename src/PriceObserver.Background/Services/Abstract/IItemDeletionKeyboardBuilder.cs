using PriceObserver.Data.Persistent.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemDeletionKeyboardBuilder
{
	InlineKeyboardMarkup Build(Item item);
}