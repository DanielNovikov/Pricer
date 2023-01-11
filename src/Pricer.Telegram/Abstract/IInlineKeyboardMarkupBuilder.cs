using Pricer.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Telegram.Abstract;

public interface IInlineKeyboardMarkupBuilder
{
	InlineKeyboardMarkup Build(MessageKeyboard keyboard);
}