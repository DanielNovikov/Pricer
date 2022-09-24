using Pricer.Dialog.Common.Models.Callback;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Dialog.Telegram.Services.Abstract;

public interface IInlineKeyboardMarkupBuilder
{
	InlineKeyboardMarkup Build(MessageKeyboard keyboard);
}