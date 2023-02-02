using Pricer.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Bot.Telegram.Abstract;

public interface IInlineKeyboardMarkupBuilder
{
	InlineKeyboardMarkup Build(MessageKeyboard keyboard);
}